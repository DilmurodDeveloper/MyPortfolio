# 1. SDK image bilan build qilamiz
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["MyPortfolio.API/MyPortfolio.API.csproj", "MyPortfolio.API/"]
COPY ["MyPortfolio.Shared/MyPortfolio.Shared.csproj", "MyPortfolio.Shared/"]
RUN dotnet restore "MyPortfolio.API/MyPortfolio.API.csproj"

COPY . .
WORKDIR "/src/MyPortfolio.API"
RUN dotnet publish "MyPortfolio.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 2. Runtime image bilan ishga tushiramiz
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render PORT environment variable orqali port beradi
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "MyPortfolio.API.dll"]
