FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SystemCase.Api/SystemCase.Api.csproj", "SystemCase.Api/"]
COPY ["SystemCase.Infrastructure/SystemCase.Infrastructure.csproj", "SystemCase.Infrastructure/"]
COPY ["SystemCase.Domain/SystemCase.Domain.csproj", "SystemCase.Domain/"]
COPY ["SystemCase.Domain.Core/SystemCase.Domain.Core.csproj", "SystemCase.Domain.Core/"]
COPY ["SystemCase.Application/SystemCase.Application.csproj", "SystemCase.Application/"]
COPY ["SystemCase.Shared/SystemCase.Shared.csproj", "SystemCase.Shared/"]
RUN dotnet restore "SystemCase.Api/SystemCase.Api.csproj"
COPY . .
WORKDIR "/src/SystemCase.Api"
RUN dotnet build "SystemCase.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SystemCase.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SystemCase.Api.dll"]
