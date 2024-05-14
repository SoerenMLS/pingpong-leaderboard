FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy csproj and restore as distinct layers
COPY ["PING-PONG-API/PING-PONG-API/*.csproj", "./PING-PONG-API/"]
RUN dotnet restore "./PING-PONG-API/PING-PONG-API.csproj"

# Copy and publish app and libraries
COPY ["PING-PONG-API/PING-PONG-API/.", "PING-PONG-API/"]
RUN dotnet publish "PING-PONG-API/PING-PONG-API.csproj" -c Release -o /app

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "PING-PONG-API.dll"]