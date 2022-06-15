#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/PasswordGenerator.API/PasswordGenerator.API.csproj", "src/PasswordGenerator.API/"]
COPY ["src/PasswordGenerator.Core/PasswordGenerator.Core.csproj", "src/PasswordGenerator.Core/"]
RUN dotnet restore "src/PasswordGenerator.API/PasswordGenerator.API.csproj"
COPY . .
WORKDIR "/src/src/PasswordGenerator.API"
RUN dotnet build "PasswordGenerator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PasswordGenerator.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "PasswordGenerator.API.dll"]