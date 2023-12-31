#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Banking.WebApi/Banking.WebApi.csproj", "Banking.WebApi/"]
COPY ["Banking.Infrastructure/Banking.Infrastructure.csproj", "Banking.Infrastructure/"]
COPY ["Banking.Application/Banking.Application.csproj", "Banking.Application/"]
COPY ["Banking.Domain/Banking.Domain.csproj", "Banking.Domain/"]
RUN dotnet restore "Banking.WebApi/Banking.WebApi.csproj"
COPY . .
WORKDIR "/src/Banking.WebApi"
RUN dotnet build "Banking.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Banking.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banking.WebApi.dll"]