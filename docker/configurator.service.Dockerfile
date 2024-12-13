# docker/configurator.service.Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["configurator.service/configurator.service.csproj", "configurator.service/"]
RUN dotnet restore "configurator.service/configurator.service.csproj"
COPY . .
WORKDIR "/src/configurator.service"
RUN dotnet build "configurator.service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "configurator.service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "configurator.service.dll"]
