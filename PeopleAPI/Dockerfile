# Use base ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5003

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the PostAPI project files
COPY ["PeopleAPI/PeopleAPI.csproj", "./"]

# Restore dependencies
RUN dotnet restore "PeopleAPI.csproj"

# Copy the rest of the PostAPI source files
COPY PeopleAPI/ .

# Build the project
RUN dotnet build "PeopleAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "PeopleAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PeopleAPI.dll"]
