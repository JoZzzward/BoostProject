FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/BoostProject.ChatsApi/BoostProject.ChatsApi.csproj", "Systems/Api/BoostProject.ChatsApi/"]
COPY ["Services/BoostProject.Services.MessagesService/BoostProject.Services.MessagesService.csproj", "Services/BoostProject.Services.MessagesService/"]
COPY ["Data/BoostProject.Data.Context/BoostProject.Data.Context.csproj", "Data/BoostProject.Data.Context/"]
COPY ["Shared/BoostProject.Settings/BoostProject.Settings.csproj", "Shared/BoostProject.Settings/"]
COPY ["Data/BoostProject.Data.Entities/BoostProject.Data.Entities.csproj", "Data/BoostProject.Data.Entities/"]
COPY ["Shared/BoostProject.Common/BoostProject.Common.csproj", "Shared/BoostProject.Common/"]
COPY ["Shared/BoostProject.Errors/BoostProject.Errors.csproj", "Shared/BoostProject.Errors/"]
COPY ["Services/BoostProject.Services.Shared/BoostProject.Services.Shared.csproj", "Services/BoostProject.Services.Shared/"]
COPY ["Systems/BoostProject.Systems.Configuration/BoostProject.Systems.Configuration.csproj", "Systems/BoostProject.Systems.Configuration/"]
RUN dotnet restore "Systems/Api/BoostProject.ChatsApi/BoostProject.ChatsApi.csproj"
COPY . .
WORKDIR "/src/Systems/Api/BoostProject.ChatsApi"
RUN dotnet build "BoostProject.ChatsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoostProject.ChatsApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoostProject.ChatsApi.dll"]