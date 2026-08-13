FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["GameOnlineStore/GameOnlineStore.csproj", "GameOnlineStore/"]
COPY ["OnlineShop.Db/GameOnlineStore.Db.csproj", "OnlineShop.Db/"]
COPY ["GameOnlineStore.Data/GameOnlineStore.Data.csproj", "GameOnlineStore.Data/"]
RUN dotnet restore "GameOnlineStore/GameOnlineStore.csproj"
COPY . .
WORKDIR /src/GameOnlineStore
RUN dotnet publish "GameOnlineStore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
USER root
RUN mkdir -p /app/logs /app/App_Data/MailPickup && chown -R $APP_UID /app
COPY --from=build --chown=$APP_UID:$APP_UID /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "GameOnlineStore.dll"]
