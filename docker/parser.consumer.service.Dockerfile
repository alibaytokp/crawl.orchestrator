FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["parser.consumer.service/parser.consumer.service.csproj", "parser.consumer.service/"]
RUN dotnet restore "parser.consumer.service/parser.consumer.service.csproj"
COPY . .
WORKDIR "/src/parser.consumer.service"
RUN dotnet build "parser.consumer.service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "parser.consumer.service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "parser.consumer.service.dll"]
