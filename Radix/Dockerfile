#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Radix.WebApi/Radix.WebApi.csproj", "src/Radix.WebApi/"]
COPY ["tests/Radix.Events.Application.Tests/Radix.Events.Application.Tests.csproj", "tests/Radix.Events.Application.Tests/"]
COPY ["tests/Radix.Events.Domain.Tests/Radix.Events.Domain.Tests.csproj", "tests/Radix.Events.Domain.Tests/"]
COPY ["src/Radix.Events.Application/Radix.Events.Application.csproj", "src/Radix.Events.Application/"]
COPY ["src/Radix.Core/Radix.Core.csproj", "src/Radix.Core/"]
COPY ["src/Radix.Events.Domain/Radix.Events.Domain.csproj", "src/Radix.Events.Domain/"]
COPY ["src/Radix.Events.Data/Radix.Events.Data.csproj", "src/Radix.Events.Data/"]

RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

RUN dotnet restore "src/Radix.WebApi/Radix.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Radix.WebApi"
RUN dotnet build "Radix.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Radix.WebApi.csproj" -c Release -o /app/publish

EXPOSE 5000

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Radix.WebApi.dll"]
