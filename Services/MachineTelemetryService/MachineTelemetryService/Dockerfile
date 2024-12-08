# Base runtime image for the application
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image for compiling the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["MachineTelemetryService.csproj", "./"]
RUN dotnet restore "MachineTelemetryService.csproj"

# Copy all source code into the container
COPY . .

# Build the application
WORKDIR "/src"
RUN dotnet build "MachineTelemetryService.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "MachineTelemetryService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app

# Copy the published app from the build stage
COPY --from=publish /app/publish .

# Set entry point to run the application
ENTRYPOINT ["dotnet", "MachineTelemetryService.dll"]
