# ==========================================
# Etapa 1: Compilación y Publicación
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# CORRECCIÓN: Apuntar a la subcarpeta del proyecto cliente
COPY ["SistemaMaquila.Client/SistemaMaquila.Client.csproj", "SistemaMaquila.Client/"]
RUN dotnet restore "SistemaMaquila.Client/SistemaMaquila.Client.csproj"

# Copiar todo el código de la solución
COPY . .

# Moverse a la carpeta del cliente antes de compilar
WORKDIR "/src/SistemaMaquila.Client"
RUN dotnet build "SistemaMaquila.Client.csproj" -c Release -o /app/build

# Publicar el proyecto
FROM build AS publish
RUN dotnet publish "SistemaMaquila.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ==========================================
# Etapa 2: Servidor Web para Producción
# ==========================================
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

COPY --from=publish /app/publish/wwwroot .

# Si tienes un nginx.conf personalizado, la ruta correcta desde la raíz de la solución sería:
# COPY SistemaMaquila.Client/nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]