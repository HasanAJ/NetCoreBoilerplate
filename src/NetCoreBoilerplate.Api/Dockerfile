FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/NetCoreBoilerplate.Api/NetCoreBoilerplate.Api.csproj", "src/NetCoreBoilerplate.Api/"]
COPY ["src/NetCoreBoilerplate.Application/NetCoreBoilerplate.Application.csproj", "src/NetCoreBoilerplate.Application/"]
COPY ["src/NetCoreBoilerplate.Infrastructure/NetCoreBoilerplate.Infrastructure.csproj", "src/NetCoreBoilerplate.Infrastructure/"]
COPY ["src/NetCoreBoilerplate.Domain/NetCoreBoilerplate.Domain.csproj", "src/NetCoreBoilerplate.Domain/"]
RUN dotnet restore "src/NetCoreBoilerplate.Api/NetCoreBoilerplate.Api.csproj"
COPY . .
WORKDIR "/src/src/NetCoreBoilerplate.Api"
RUN dotnet build "NetCoreBoilerplate.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreBoilerplate.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreBoilerplate.Api.dll"]
