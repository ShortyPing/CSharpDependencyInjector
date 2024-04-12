FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DependencyInjectorUsage/DependencyInjectorUsage.csproj", "DependencyInjectorUsage/"]
RUN dotnet restore "DependencyInjectorUsage/DependencyInjectorUsage.csproj"
COPY . .
WORKDIR "/src/DependencyInjectorUsage"
RUN dotnet build "DependencyInjectorUsage.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DependencyInjectorUsage.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DependencyInjectorUsage.dll"]
