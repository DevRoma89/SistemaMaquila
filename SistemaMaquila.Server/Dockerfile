# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar solo los proyectos para cache de restore
COPY ["SistemaMaquila.Server/SistemaMaquila.Server.csproj", "SistemaMaquila.Server/"]

# Restaurar dependencias
RUN dotnet restore "SistemaMaquila.Server/SistemaMaquila.Server.csproj"

# Copiar todo el código
COPY . .

WORKDIR "/src/SistemaMaquila.Server"

# Publicar
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "SistemaMaquila.Server.dll"]