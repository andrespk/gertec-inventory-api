FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Gertec.Inventory.Management/Gertec.Inventory.Management.csproj", "Gertec.Inventory.Management/"]
RUN dotnet restore "Gertec.Inventory.Management/Gertec.Inventory.Management.csproj"
COPY . .
WORKDIR "/src/Gertec.Inventory.Management"
RUN dotnet build "Gertec.Inventory.Management.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Gertec.Inventory.Management.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gertec.Inventory.Management.dll"]
