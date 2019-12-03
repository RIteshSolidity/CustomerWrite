FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["OrderWrite/OrderWrite.csproj", "OrderWrite/"]
RUN dotnet restore "OrderWrite/OrderWrite.csproj"
COPY . .
WORKDIR "/src/OrderWrite"
RUN dotnet build "OrderWrite.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderWrite.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderWrite.dll"]