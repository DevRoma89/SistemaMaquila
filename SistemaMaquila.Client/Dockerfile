# ==========================================
# Etapa 1: Compilación y Publicación
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
# (Cambia "TuProyecto.csproj" por el nombre real de tu archivo)
COPY ["SistemaMaquila.Client.csproj", "./"]
RUN dotnet restore "./SistemaMaquila.Client.csproj"

# Copiar el resto del código y compilar en modo Release
COPY . .
RUN dotnet build "SistemaMaquila.Client.csproj" -c Release -o /app/build

# Publicar el proyecto para generar los archivos estáticos de WASM
FROM build AS publish
RUN dotnet publish "SistemaMaquila.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ==========================================
# Etapa 2: Servidor Web para Producción
# ==========================================
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Copiar los archivos estáticos generados en la etapa de publicación
# Nginx por defecto sirve el contenido desde /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .

# Copiar una configuración personalizada de Nginx para manejar rutas de SPA (opcional pero recomendado)
# COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]