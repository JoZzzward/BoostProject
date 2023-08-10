FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/EmailWorker/BoostProject.EmailWorker/BoostProject.EmailWorker.csproj", "Systems/EmailWorker/BoostProject.EmailWorker/"]
COPY ["Data/BoostProject.Data.Context/BoostProject.Data.Context.csproj", "Data/BoostProject.Data.Context/"]
COPY ["Shared/BoostProject.Settings/BoostProject.Settings.csproj", "Shared/BoostProject.Settings/"]
COPY ["Data/BoostProject.Data.Entities/BoostProject.Data.Entities.csproj", "Data/BoostProject.Data.Entities/"]
COPY ["Shared/BoostProject.Common/BoostProject.Common.csproj", "Shared/BoostProject.Common/"]
COPY ["Services/BoostProject.Services.Actions/BoostProject.Services.Actions.csproj", "Services/BoostProject.Services.Actions/"]
COPY ["Services/BoostProject.Services.EmailSender/BoostProject.Services.EmailSender.csproj", "Services/BoostProject.Services.EmailSender/"]
COPY ["Services/BoostProject.Services.RabbitMqService/BoostProject.Services.RabbitMqService.csproj", "Services/BoostProject.Services.RabbitMqService/"]
RUN dotnet restore "Systems/EmailWorker/BoostProject.EmailWorker/BoostProject.EmailWorker.csproj"
COPY . .
WORKDIR "/src/Systems/EmailWorker/BoostProject.EmailWorker"
RUN dotnet build "BoostProject.EmailWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoostProject.EmailWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoostProject.EmailWorker.dll"]