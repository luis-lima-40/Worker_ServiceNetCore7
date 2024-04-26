#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Worker_ServiceNetCore7/Worker_ServiceNetCore7.csproj", "Worker_ServiceNetCore7/"]
RUN dotnet restore "./Worker_ServiceNetCore7/Worker_ServiceNetCore7.csproj"
COPY . .
WORKDIR "/src/Worker_ServiceNetCore7"
RUN dotnet build "./Worker_ServiceNetCore7.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Worker_ServiceNetCore7.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Worker_ServiceNetCore7.dll"]