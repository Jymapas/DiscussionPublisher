FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Копирование .sln и .csproj и восстановление зависимостей
COPY . .
RUN dotnet restore DiscussionPublisher.sln

# Сборка проекта
RUN dotnet publish ./DiscussionPublisherAPI/DiscussionPublisherAPI.csproj -c Release -o out

# Создание образа времени выполнения
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "DiscussionPublisherAPI.dll"]
