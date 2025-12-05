FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Identidade.Api/Identidade.Api.csproj", "Identidade.Api/"]
COPY ["Identidade.Application/Identidade.Application.csproj", "Identidade.Application/"]
COPY ["Identidade.Domain/Identidade.Domain.csproj", "Identidade.Domain/"]
COPY ["Identidade.Infrastructure/Identidade.Infrastructure.csproj", "Identidade.Infrastructure/"]
RUN dotnet restore "Identidade.Api/Identidade.Api.csproj"
COPY . .
WORKDIR "/src/Identidade.Api"
RUN dotnet build "Identidade.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identidade.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identidade.Api.dll"]
