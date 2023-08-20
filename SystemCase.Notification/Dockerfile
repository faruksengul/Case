FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SystemCase.Notification/SystemCase.Notification.csproj", "SystemCase.Notification/"]
COPY ["SystemCase.Shared/SystemCase.Shared.csproj", "SystemCase.Shared/"]
RUN dotnet restore "SystemCase.Notification/SystemCase.Notification.csproj"
COPY . .
WORKDIR "/src/SystemCase.Notification"
RUN dotnet build "SystemCase.Notification.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SystemCase.Notification.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SystemCase.Notification.dll"]
