FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Expose ports (padrão .NET + debug)
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Cache de dependências - Melhora performance do build
COPY ["src/TesteMeli.Api/TesteMeli.Api.csproj", "src/TesteMeli.Api/"]
COPY ["src/TesteMeli.Business/TesteMeli.Business.csproj", "src/TesteMeli.Business/"]
COPY ["src/TesteMeli.Data/TesteMeli.Data.csproj", "src/TesteMeli.Data/"]

RUN dotnet restore "src/TesteMeli.Api/TesteMeli.Api.csproj"

COPY . .
WORKDIR "/src/src/TesteMeli.Api"

# Build com otimizações
RUN dotnet build "TesteMeli.Api.csproj" -c Release -o /app/build \
    --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish "TesteMeli.Api.csproj" -c Release -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Labels para documentação
LABEL maintainer="email_aqui@email.com"
LABEL description="API TesteMeli"
LABEL version="1.0.0"

# Copia os arquivos publicados
COPY --from=publish /app/publish .

# Diretório para dados da aplicação (importante para seu JSON repository)
RUN mkdir -p /app/data && \
    chmod 755 /app/data

USER $APP_UID

# Variáveis de ambiente para configuração
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true

ENTRYPOINT ["dotnet", "TesteMeli.Api.dll"]