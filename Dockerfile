FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY src/ContosoBookstore.Common/*.csproj src/ContosoBookstore.Common/
COPY src/ContosoBookstore.Api/*.csproj src/ContosoBookstore.Api/
RUN dotnet restore src/ContosoBookstore.Api/ContosoBookstore.Api.csproj

# Copy everything else and publish
COPY src/ src/
RUN dotnet publish src/ContosoBookstore.Api/ContosoBookstore.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "ContosoBookstore.Api.dll"]
