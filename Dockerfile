# Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080


# Build Image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy .csproj and restore as distinct layers
COPY ["YT.Northwind.WebAPI/Northwind.WebAPI.csproj", "YT.Northwind.WebAPI/"]
COPY ["YT.Northwind.Business/Northwind.Business.csproj", "YT.Northwind.Business/"]
COPY ["YT.Northwind.DataAccess/Northwind.DataAccess.csproj", "YT.Northwind.DataAccess/"]
COPY ["YT.Northwind.Entities/Northwind.Entities.csproj", "YT.Northwind.Entities/"]
COPY ["YT.Northwind.Core/Northwind.Core.csproj", "YT.Northwind.Core/"]
COPY ["YT.Northwind.Product.Consumer/Northwind.Product.Consumer.csproj", "YT.Northwind.Product.Consumer/"]

RUN dotnet restore "YT.Northwind.WebAPI/Northwind.WebAPI.csproj"

# Copy everything else and build app
COPY . .
WORKDIR "/src/YT.Northwind.WebAPI"
RUN dotnet build "Northwind.WebAPI.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "Northwind.WebAPI.csproj" -c Release -o /app/publish

# Final Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Use only the DLL name in ENTRYPOINT
ENTRYPOINT ["dotnet", "Northwind.WebAPI.dll"]
