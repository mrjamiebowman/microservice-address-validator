FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AddressValidator.Api/AddressValidator.Api.csproj", "AddressValidator.Api/"]
COPY ["AddressValidator.Data/AddressValidator.Data.csproj", "AddressValidator.Data/"]
COPY ["AddressValidator.Data.Models/AddressValidator.Data.Models.csproj", "AddressValidator.Data.Models/"]
RUN dotnet restore "AddressValidator.Api/AddressValidator.Api.csproj"
COPY . .
WORKDIR "/src/AddressValidator.Api"
RUN dotnet build "AddressValidator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AddressValidator.Api.csproj" -c Release -o /app/publish

FROM base AS final
RUN adduser --uid 1001 --disabled-password --no-create-home userms
USER userms
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AddressValidator.Api.dll"]