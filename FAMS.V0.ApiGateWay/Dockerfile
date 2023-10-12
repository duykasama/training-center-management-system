FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FAMS.V0.ApiGateWay/FAMS.V0.ApiGateWay.csproj", "FAMS.V0.ApiGateWay/"]
RUN dotnet restore "FAMS.V0.ApiGateWay/FAMS.V0.ApiGateWay.csproj"
COPY . .
WORKDIR "/src/FAMS.V0.ApiGateWay"
RUN dotnet build "FAMS.V0.ApiGateWay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FAMS.V0.ApiGateWay.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FAMS.V0.ApiGateWay.dll"]
