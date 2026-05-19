# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copie du .csproj et restauration des dépendances (cache optimisé)
COPY JiraLowCost.csproj .
RUN dotnet restore

# Copie du reste du code et publication
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copie des fichiers publiés depuis l'étape build
COPY --from=build /app/publish .

# Utilisateur non-root pour la sécurité
USER app

ENTRYPOINT ["dotnet", "JiraLowCost.dll"]