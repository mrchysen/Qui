# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble AS base
WORKDIR /app
RUN chmod 777 -R /app
EXPOSE 8080
EXPOSE 8081

# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RazorPagesApp/RazorPagesApp.csproj", "RazorPagesApp/"]
RUN dotnet restore "./RazorPagesApp/RazorPagesApp.csproj"
COPY . .
WORKDIR "/src/RazorPagesApp"
RUN dotnet build "./RazorPagesApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RazorPagesApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN chmod 777 -R /app

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RazorPagesApp.dll"]