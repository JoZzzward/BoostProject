using BoostProject.Settings.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace BoostProject.Services.RabbitMqService;

public class RabbitMqService : IRabbitMqService, IDisposable
{
    private readonly object _connectionLock = new();
    private IConnection _connection;

    private IModel _channel;
    private const string _exchangeName = "BoostProjectExchange";

    private readonly ILogger<RabbitMqService> _logger;
    private readonly IRabbitMqSettings _settings;

    public RabbitMqService(
        IRabbitMqSettings settings,
        ILogger<RabbitMqService> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task Subscribe<T>(string queueName, OnMessageReceive<T> onReceive)
    {
        if (onReceive == null)
            return;

        await RegisterListener(queueName, async (_, eventArgs) =>
        {
            var channel = GetChannel();
            try
            {
                var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                var obj = JsonSerializer.Deserialize<T>(message ?? "");

                await onReceive(obj!);
                channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch 
            {
                channel.BasicNack(eventArgs.DeliveryTag, false, false);
            }
        });
    }

    private IModel GetChannel() => _channel;

    private async Task Publish<T>(string queueName, T data)
    {
        Connect();

        AddQueue(queueName);

        var json = JsonSerializer.Serialize<object>(data, new JsonSerializerOptions());

        var message = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish(_exchangeName, queueName, null, message);
    }

    private async Task RegisterListener(string queueName, EventHandler<BasicDeliverEventArgs> onReceive)
    {
        Connect();

        AddQueue(queueName);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += onReceive;

        _channel.BasicConsume(queueName, false, consumer);

        _logger.LogInformation("--> Subscribes on listening in queue(Name: {QueueName})", queueName);
    }

    public async Task PushAsync<T>(string queueName, T data)
    {
        await Publish(queueName, data);
    }

    private void AddQueue(string queueName)
    {
        Connect();

        _channel.QueueDeclare(queueName, true, false, false, null);
    }

    private void Connect()
    {
        lock (_connectionLock)
        {
            if (_connection?.IsOpen ?? false)
                return;

            var factory = new ConnectionFactory
            {
                Uri = new Uri(_settings.Uri),
                UserName = _settings.UserName,
                Password = _settings.Password,

                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(5)
            };

            for (int retriesCount = 1; retriesCount <= 15; retriesCount++)
            {
                try
                {
                    _logger.LogInformation("Trying connect to RabbitMQ. Retries count: {RetriesCount}", retriesCount);

                    _connection ??= factory.CreateConnection();

                    if(_channel == null)
                    {
                        _channel ??= _connection.CreateModel();

                        _channel.BasicQos(0, 1, false);
                    }

                    break;
                }
                catch (BrokerUnreachableException)
                {
                    Task.Delay(1000).Wait();
                }
            }
        }
    }

    public void Dispose()
    {
        _connection?.Dispose();
        _channel?.Dispose();
    }
}
