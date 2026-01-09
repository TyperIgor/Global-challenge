# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /Device.API
COPY ["Device.API/Device.API/Device.API.csproj", "Device.API/"]
COPY ["Device.API/Device.API.Application.Message/Device.API.Application.Message.csproj", "Device.API.Application.Message/"]
COPY ["Device.API/Device.API.Domain.Models/Device.API.Domain.Models.csproj", "Device.API.Domain.Models/"]
COPY ["Device.API/Device.API.Infrastructure.Utils/Device.API.Infrastructure.Utils.csproj", "Device.API.Infrastructure.Utils/"]
COPY ["Device.API/Device.API.Application.Service/Device.API.Application.Service.csproj", "Device.API.Application.Service/"]
COPY ["Device.API/Device.API.Domain.Contracts/Device.API.Domain.Contracts.csproj", "Device.API.Domain.Contracts/"]
COPY ["Device.API/Device.API.Infrastructure.DI/Device.API.Infrastructure.DI.csproj", "Device.API.Infrastructure.DI/"]
COPY ["Device.API/Device.API.Domain.Service/Device.API.Domain.Service.csproj", "Device.API.Domain.Service/"]
COPY ["Device.API/Device.API.Infrastructure.Data/Device.API.Infrastructure.Data.csproj", "Device.API.Infrastructure.Data/"]
RUN dotnet restore "./Device.API/Device.API.csproj"
COPY . .

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Device.API/./Device.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Device.API.dll"]