#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/AddressValidator.Api/AddressValidator.Api.csproj", "src/AddressValidator.Api/"]
COPY ["src/AddressValidator.Data/AddressValidator.Data.csproj", "src/AddressValidator.Data/"]
COPY ["src/AddressValidator.Data.Models/AddressValidator.Data.Models.csproj", "src/AddressValidator.Data.Models/"]
RUN dotnet restore "src/AddressValidator.Api/AddressValidator.Api.csproj"
COPY . .
WORKDIR "/src/src/AddressValidator.Api"
RUN dotnet build "AddressValidator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AddressValidator.Api.csproj" -c Release -o /app/publish

FROM base AS final
RUN useradd -u 1001 userms
USER userms
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AddressValidator.Api.dll"]