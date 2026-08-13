FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["GameOnlineShop/GameOnlineShop.csproj", "GameOnlineShop/"]
COPY ["OnlineShop.Db/GameOnlineShop.Db.csproj", "OnlineShop.Db/"]
COPY ["GameOnlineShop.Data/GameOnlineShop.Data.csproj", "GameOnlineShop.Data/"]
RUN dotnet restore "GameOnlineShop/GameOnlineShop.csproj"
COPY . .
WORKDIR /src/GameOnlineShop
RUN dotnet publish "GameOnlineShop.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
USER root
RUN mkdir -p /app/logs /app/App_Data/MailPickup && chown -R $APP_UID /app
COPY --from=build --chown=$APP_UID:$APP_UID /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "GameOnlineShop.dll"]
