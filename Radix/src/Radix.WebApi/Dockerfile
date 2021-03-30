#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["src/Radix.WebApi/Radix.WebApi.csproj", "src/Radix.WebApi/"]
COPY ["tests/Radix.Events.Application.Tests/Radix.Events.Application.Tests.csproj", "tests/Radix.Events.Application.Tests/"]
COPY ["src/Radix.Events.Application/Radix.Events.Application.csproj", "src/Radix.Events.Application/"]
COPY ["src/Radix.Core/Radix.Core.csproj", "src/Radix.Core/"]
COPY ["src/Radix.Events.Domain/Radix.Events.Domain.csproj", "src/Radix.Events.Domain/"]
COPY ["src/Radix.Events.Data/Radix.Events.Data.csproj", "src/Radix.Events.Data/"]
RUN dotnet restore "src/Radix.WebApi/Radix.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Radix.WebApi"
RUN dotnet build "Radix.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Radix.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Radix.WebApi.dll"]