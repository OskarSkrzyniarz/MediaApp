FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SocialMediaApp.csproj", "./"]
RUN dotnet restore "./SocialMediaApp.csproj"
COPY . .
RUN dotnet build "SocialMediaApp.csproj" -c Release -o /app/build
RUN dotnet publish "SocialMediaApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "SocialMediaApp.dll"]