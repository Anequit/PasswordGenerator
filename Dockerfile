FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY PasswordGenerator.sln .
COPY src/PasswordGenerator.API/*.csproj ./PasswordGenerator.API/
COPY src/PasswordGenerator.Core/*.csproj ./PasswordGenerator.Core/

COPY . .

RUN dotnet publish ./src/PasswordGenerator.API -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app .
ENTRYPOINT ["dotnet", "PasswordGenerator.API.dll"]