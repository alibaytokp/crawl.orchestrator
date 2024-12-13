FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/parser.api.service/parser.api.service.csproj", "src/parser.api.service/"]
COPY ["src/messaging.service/messaging.service.csproj", "src/messaging.service/"]
RUN dotnet restore "./src/parser.api.service/parser.api.service.csproj"
COPY . .
WORKDIR "/src/src/parser.api.service"
RUN dotnet build "./parser.api.service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./parser.api.service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "parser.api.service.dll"]