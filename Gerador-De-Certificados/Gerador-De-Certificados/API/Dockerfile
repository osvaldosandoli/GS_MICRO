# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copia os arquivos de projeto e restaura as dependências
#COPY ../*.csproj ./  # Certifique-se de que este caminho está correto
RUN dotnet restore

# Copia todo o restante dos arquivos
#COPY ../Gerador-De-Certificados\API

# Build da aplicação
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out ./

# Define a porta que o aplicativo vai escutar
EXPOSE 80
ENTRYPOINT ["dotnet", "Gerador-De-Certificado.dll"]
