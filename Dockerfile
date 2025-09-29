FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

#Copia de todo el codigo 
COPY *.csproj ./
RUN dotnet restore

#Compilar y publicar
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime 
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "perla-metro-tickets-service.dll"]