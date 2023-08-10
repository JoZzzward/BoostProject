FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/BoostProject.Api/BoostProject.Api.csproj", "Systems/Api/BoostProject.Api/"]
COPY ["Services/BoostProject.Services.Actions/BoostProject.Services.Actions.csproj", "Services/BoostProject.Services.Actions/"]
COPY ["Shared/BoostProject.Common/BoostProject.Common.csproj", "Shared/BoostProject.Common/"]
COPY ["Shared/BoostProject.Settings/BoostProject.Settings.csproj", "Shared/BoostProject.Settings/"]
COPY ["Services/BoostProject.Services.EmailSender/BoostProject.Services.EmailSender.csproj", "Services/BoostProject.Services.EmailSender/"]
COPY ["Services/BoostProject.Services.RabbitMqService/BoostProject.Services.RabbitMqService.csproj", "Services/BoostProject.Services.RabbitMqService/"]
COPY ["Services/BoostProject.Services.FeedbackService/BoostProject.Services.FeedbackService.csproj", "Services/BoostProject.Services.FeedbackService/"]
COPY ["Shared/BoostProject.Errors/BoostProject.Errors.csproj", "Shared/BoostProject.Errors/"]
COPY ["Services/BoostProject.Services.Shared/BoostProject.Services.Shared.csproj", "Services/BoostProject.Services.Shared/"]
COPY ["Data/BoostProject.Data.Context/BoostProject.Data.Context.csproj", "Data/BoostProject.Data.Context/"]
COPY ["Data/BoostProject.Data.Entities/BoostProject.Data.Entities.csproj", "Data/BoostProject.Data.Entities/"]
COPY ["Services/BoostProject.Services.GameAccountService/BoostProject.Services.GameAccountService.csproj", "Services/BoostProject.Services.GameAccountService/"]
COPY ["Services/BoostProject.Services.MessagesService/BoostProject.Services.MessagesService.csproj", "Services/BoostProject.Services.MessagesService/"]
COPY ["Services/BoostProject.Services.OrderService/BoostProject.Services.OrderService.csproj", "Services/BoostProject.Services.OrderService/"]
COPY ["Services/BoostProject.Services.UserAccountService/BoostProject.Services.UserAccountService.csproj", "Services/BoostProject.Services.UserAccountService/"]
COPY ["Systems/BoostProject.Systems.Configuration/BoostProject.Systems.Configuration.csproj", "Systems/BoostProject.Systems.Configuration/"]
RUN dotnet restore "Systems/Api/BoostProject.Api/BoostProject.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/BoostProject.Api"
RUN dotnet build "BoostProject.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoostProject.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoostProject.Api.dll"]