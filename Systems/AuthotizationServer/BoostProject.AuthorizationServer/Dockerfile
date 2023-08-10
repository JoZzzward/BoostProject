FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/AuthotizationServer/BoostProject.AuthorizationServer/BoostProject.AuthorizationServer.csproj", "Systems/AuthotizationServer/BoostProject.AuthorizationServer/"]
COPY ["Data/BoostProject.Data.Context/BoostProject.Data.Context.csproj", "Data/BoostProject.Data.Context/"]
COPY ["Shared/BoostProject.Settings/BoostProject.Settings.csproj", "Shared/BoostProject.Settings/"]
COPY ["Data/BoostProject.Data.Entities/BoostProject.Data.Entities.csproj", "Data/BoostProject.Data.Entities/"]
COPY ["Shared/BoostProject.Common/BoostProject.Common.csproj", "Shared/BoostProject.Common/"]
COPY ["Services/BoostProject.Services.UserAccountService/BoostProject.Services.UserAccountService.csproj", "Services/BoostProject.Services.UserAccountService/"]
COPY ["Shared/BoostProject.Errors/BoostProject.Errors.csproj", "Shared/BoostProject.Errors/"]
COPY ["Services/BoostProject.Services.EmailSender/BoostProject.Services.EmailSender.csproj", "Services/BoostProject.Services.EmailSender/"]
RUN dotnet restore "Systems/AuthotizationServer/BoostProject.AuthorizationServer/BoostProject.AuthorizationServer.csproj"
COPY . .
WORKDIR "/src/Systems/AuthotizationServer/BoostProject.AuthorizationServer"
RUN dotnet build "BoostProject.AuthorizationServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoostProject.AuthorizationServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoostProject.AuthorizationServer.dll"]