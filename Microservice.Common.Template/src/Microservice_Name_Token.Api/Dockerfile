# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Microservice_Name_Token.Api/*.csproj ./Microservice_Name_Token.Api/
RUN dotnet restore

# Copy everything else and build
COPY Microservice_Name_Token.Api/. ./Microservice_Name_Token.Api/
WORKDIR /src/Microservice_Name_Token.Api
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Microservice_Name_Token.Api.dll"]
