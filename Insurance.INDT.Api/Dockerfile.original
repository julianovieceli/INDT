#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Insurance.INDT.Api/Insurance.INDT.Api.csproj", "Insurance.INDT.Api/"]
COPY ["Insurance.INDT.Application/Insurance.INDT.Application.csproj", "Insurance.INDT.Application/"]
COPY ["INDT.Common/INDT.Common.Insurance.Domain/INDT.Common.Insurance.Domain.csproj", "INDT.Common/INDT.Common.Insurance.Domain/"]
COPY ["INDT.Common/INDT.Common.Insurance.Dto/INDT.Common.Insurance.Dto.csproj", "INDT.Common/INDT.Common.Insurance.Dto/"]
COPY ["Insurance.INDT.Mysql.Repository/Insurance.INDT.Infra.Mysql.Repository.csproj", "Insurance.INDT.Mysql.Repository/"]
COPY ["INDT.Common/INDT.Common.Insurance.Infra.Mysql.Repository/INDT.Common.Insurance.Infra.Mysql.Repository.csproj", "INDT.Common/INDT.Common.Insurance.Infra.Mysql.Repository/"]
RUN dotnet restore "./Insurance.INDT.Api/Insurance.INDT.Api.csproj"
COPY . .
WORKDIR "/src/Insurance.INDT.Api"
RUN dotnet build "./Insurance.INDT.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Insurance.INDT.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Insurance.INDT.Api.dll"]