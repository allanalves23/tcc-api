FROM mcr.microsoft.com/dotnet/core/sdk:3.1

EXPOSE 80

WORKDIR /app

COPY /web-api-tcc.sln /app/web-api-tcc.sln
COPY /01_Presentation/API/API.csproj /app/01_Presentation/API/API.csproj
COPY /02_DependencyInjection/02_DependencyInjection.csproj /app/02_DependencyInjection/02_DependencyInjection.csproj
COPY /03_Domain/Core/Core.csproj /app/03_Domain/Core/Core.csproj
COPY /03_Domain/Services/Services.csproj /app/03_Domain/Services/Services.csproj
COPY /04_Infrastructure/Repository/Repository.csproj /app/04_Infrastructure/Repository/Repository.csproj

RUN dotnet restore

WORKDIR /app
COPY . .

RUN dotnet build

WORKDIR /app/01_Presentation/API
RUN dotnet publish -c Release

WORKDIR /app/01_Presentation/API/bin/Release/netcoreapp3.1/publish

ENTRYPOINT [ "dotnet", "API.dll"]