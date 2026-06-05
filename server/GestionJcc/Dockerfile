# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solo los proyectos para cache de restore
COPY ["GestionJcc/GestionJcc.csproj", "GestionJcc/"]

# Restaurar dependencias
RUN dotnet restore "GestionJcc/GestionJcc.csproj"

# Copiar todo el código
COPY . .

WORKDIR "/src/GestionJcc"

# Publicar
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "GestionJcc.dll"]