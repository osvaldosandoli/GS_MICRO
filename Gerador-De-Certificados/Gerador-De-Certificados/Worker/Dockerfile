# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia o arquivo do projeto (ajuste o caminho para o csproj)
COPY ../Gerador-De-Certificados.csproj ./

# Restaura as dependências
RUN dotnet restore

# Copia todos os arquivos da pasta Worker
COPY . ./

# Build da aplicação
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build-env /app/out ./

# Define a porta que o aplicativo vai escutar
EXPOSE 80
ENTRYPOINT ["dotnet", "Gerador-De-Certificados.dll"]
