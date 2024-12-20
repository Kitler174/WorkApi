# U�yj oficjalnego obrazu .NET SDK jako obrazu bazowego do budowania aplikacji
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Skopiuj plik projektu i przywr�� zale�no�ci
COPY ["WorkAPI.csproj", "./"]
RUN dotnet restore

# Skopiuj pozosta�e pliki aplikacji
COPY . .

# Zbuduj aplikacj�
RUN dotnet build -c Release -o /app/build

# Publikacja aplikacji
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# U�yj oficjalnego obrazu .NET Runtime jako bazowego do uruchomienia aplikacji
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Otw�rz porty, na kt�rych aplikacja b�dzie dzia�a�
EXPOSE 8080
EXPOSE 9080

# Komenda uruchamiaj�ca aplikacj�
ENTRYPOINT ["dotnet", "WorkAPI.dll"]
