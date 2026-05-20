# in single stage the image contains restore, build, publish, sdk, etc.
# so the image becomes larger although we only need runtime

# Stage 1 => Build

# Base image for .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project file and restore packages
COPY *.csproj ./

RUN dotnet restore

# Copy source code
COPY . .

# Publish app
RUN dotnet publish -c Release -o /app/publish

# Stage 2 => Runtime

# Lighter runtime-only image
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Run application
ENTRYPOINT ["dotnet", "Labs.WebApiApp.dll"]