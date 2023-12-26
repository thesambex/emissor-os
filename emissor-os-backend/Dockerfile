# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source
COPY . .

RUN dotnet restore "./Emissor.API/Emissor.API.csproj" --disable-parallel
RUN dotnet publish "./Emissor.API/Emissor.API.csproj" -c release -o /app --no-restore

#Serve
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Emissor.API.dll"]