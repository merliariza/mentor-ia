# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copiar los archivos de proyectos
COPY ["Apieducation/Apieducation.csproj", "Apieducation/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "Apieducation/Apieducation.csproj"

# Copiar el resto del código
COPY . .

# Compilar
WORKDIR "/src/Apieducation"
RUN dotnet build "Apieducation.csproj" -c Release -o /app/build

# Publicar
RUN dotnet publish "Apieducation.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

# Render
ENV ASPNETCORE_URLS=http://0.0.0.0:10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "Apieducation.dll"]