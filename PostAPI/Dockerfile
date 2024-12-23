# Use base ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the PostAPI project files
COPY ["PostAPI/PostAPI.csproj", "./"]

# Restore dependencies
RUN dotnet restore "PostAPI.csproj"

# Copy the rest of the PostAPI source files
COPY PostAPI/ .

# Build the project
RUN dotnet build "PostAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "PostAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostAPI.dll"]
