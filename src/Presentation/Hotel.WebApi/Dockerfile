#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Presentation/Hotel.WebApi/Hotel.WebApi.csproj", "src/Presentation/Hotel.WebApi/"]
COPY ["src/Core/Hotel.Application/Hotel.Core.Application.csproj", "src/Core/Hotel.Application/"]
COPY ["src/Core/Hotel.Domain/Hotel.Core.Domain.csproj", "src/Core/Hotel.Domain/"]
COPY ["src/Infrastructure/Hotel.Identity/Hotel.Infrastructure.Identity.csproj", "src/Infrastructure/Hotel.Identity/"]
COPY ["src/Infrastructure/Hotel.Infrastructure.Shared/Hotel.Infrastructure.Utils.csproj", "src/Infrastructure/Hotel.Infrastructure.Shared/"]
COPY ["src/Infrastructure/Hotel.Persistence/Hotel.Infrastructure.Persistence.csproj", "src/Infrastructure/Hotel.Persistence/"]
RUN dotnet restore "src/Presentation/Hotel.WebApi/Hotel.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Hotel.WebApi"
RUN dotnet build "Hotel.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hotel.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hotel.WebApi.dll"]