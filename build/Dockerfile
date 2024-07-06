FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Gertec.Inventory.Management.Api/Gertec.Inventory.Management.Api.csproj", "build/Gertec.Inventory.Management.Api/"]
COPY ["Gertec.Inventory.Management.Application.CommandStack/Gertec.Inventory.Management.Application.CommandStack.csproj", "build/Gertec.Inventory.Management.Application.CommandStack/"]
COPY ["Gertec.Inventory.Management.Application.QueryStack/Gertec.Inventory.Management.Application.QueryStack.csproj", "build/Gertec.Inventory.Management.Application.QueryStack/"]
COPY ["Gertec.Inventory.Management.Domain/Gertec.Inventory.Management.Domain.csproj", "build/Gertec.Inventory.Management.Domain/"]
COPY ["Gertec.Inventory.Management.Infrastructure/Gertec.Inventory.Management.Infrastructure.csproj", "build/Gertec.Inventory.Management.Infrastructure/"]
RUN dotnet restore "build/Gertec.Inventory.Management.Api/Gertec.Inventory.Management.Api.csproj"
COPY . .
RUN dotnet build "build/Gertec.Inventory.Management.Api.csproj" -c $BUILD_CONFIGURATION -o /publish

FROM base AS final
WORKDIR /api
COPY --from=build /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Gertec.Inventory.Management.Api.dll"]
