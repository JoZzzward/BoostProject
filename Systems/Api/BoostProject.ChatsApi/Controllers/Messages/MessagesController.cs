using AutoMapper;
using BoostProject.ChatsApi.Controllers.Messages.Models;
using BoostProject.Common.Consts;
using BoostProject.Common.Security;
using BoostProject.Services.MessagesService;
using BoostProject.Services.MessagesService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BoostProject.ChatsApi.Controllers.Messages;

[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
[EnableCors(PolicyName = CorsConsts.DefaultOriginName)]
[Authorize/*(Policy = AppScopes.MessageAccess)*/]
[ApiVersion("1.0")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMessagesService _messagesService;
    private readonly IMapper _mapper;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(
        IMessagesService messagesService,
        IMapper mapper,
        ILogger<MessagesController> logger
        )
    {
        _messagesService = messagesService;
        _mapper = mapper;
        _logger = logger;
    }


    [ProducesResponseType(typeof(IEnumerable<SendMessageResponse>), 200)]
    [HttpGet("{senderId}")]
    public async Task<IEnumerable<MessageResponse>> GetMessages([FromRoute] Guid senderId)
    {
        _logger.LogInformation("--> Returning all messages for user (Id: {SenderId})", senderId);

        return await _messagesService.GetMessagesBySenderId(senderId);
    }

    [ProducesResponseType(typeof(SendMessageResponse), 200)]
    [HttpDelete]
    public async Task<DeleteMessageResponse> DeleteMessage([FromBody] DeleteMessageRequest request)
    {
        _logger.LogInformation("--> Removing the message (Id: {MessageId}) for user (Id: {UserId})..", request.MessageId, request.SenderId);

        var model = _mapper.Map<DeleteMessageModel>(request);

        return await _messagesService.DeleteMessage(model);
    }
}
