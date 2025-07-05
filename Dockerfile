FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY MyPortfolio.API/MyPortfolio.API.csproj MyPortfolio.API/
COPY MyPortfolio.Shared/MyPortfolio.Shared.csproj MyPortfolio.Shared/
RUN dotnet restore MyPortfolio.API/MyPortfolio.API.csproj

COPY . .
WORKDIR /src/MyPortfolio.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "MyPortfolio.API.dll"]