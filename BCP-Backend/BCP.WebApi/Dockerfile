FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["BCP.WebApi/BCP.WebApi.csproj", "BCP.WebApi/"]
RUN dotnet restore "BCP.WebApi/BCP.WebApi.csproj"
COPY . .
WORKDIR "/src/BCP.WebApi"
RUN dotnet build "BCP.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BCP.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BCP.WebApi.dll"]
