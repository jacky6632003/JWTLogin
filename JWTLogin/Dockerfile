#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["JWTLogin/JWTLogin.csproj", "JWTLogin/"]
COPY ["JWTLoginCommon/JWTLoginCommon.csproj", "JWTLoginCommon/"]
COPY ["JWTLogin.Service/JWTLogin.Service.csproj", "JWTLogin.Service/"]
COPY ["JWTLogin.Repository/JWTLogin.Repository.csproj", "JWTLogin.Repository/"]
RUN dotnet restore "JWTLogin/JWTLogin.csproj"
COPY . .
WORKDIR "/src/JWTLogin"
RUN dotnet build "JWTLogin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JWTLogin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWTLogin.dll"]