FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# copy all layers
COPY ["./BCP.Domain/BCP.Domain.csproj", "/src/BCP.Domain/"]
COPY ["./BCP.Application/BCP.Application.csproj", "/src/BCP.Application/"]
COPY ["./BCP.Infraestructure/BCP.Infraestructure.csproj", "/src/BCP.Infraestructure/"]
COPY ["./BCP.WebApi/BCP.WebApi.csproj", "/src/BCP.WebApi/"]

# restore dependencies
RUN dotnet restore "BCP.WebApi/BCP.WebApi.csproj"
COPY . .

# run build api 
WORKDIR "/src/BCP.WebApi"
RUN dotnet build "BCP.WebApi.csproj" -c Release -o /app/build

# run publish api
FROM build AS publish
RUN dotnet publish "BCP.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BCP.WebApi.dll"]
