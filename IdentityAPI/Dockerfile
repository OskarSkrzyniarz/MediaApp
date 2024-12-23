# Use base ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5004

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the PostAPI project files
COPY ["IdentityAPI/IdentityAPI.csproj", "./"]

# Restore dependencies
RUN dotnet restore "IdentityAPI.csproj"

# Copy the rest of the PostAPI source files
COPY IdentityAPI/ .

# Build the project
RUN dotnet build "IdentityAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "IdentityAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityAPI.dll"]
