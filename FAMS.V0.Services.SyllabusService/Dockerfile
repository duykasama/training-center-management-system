FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./FAMS.V0.Services.SyllabusService.csproj", "FAMS.V0.Services.SyllabusService/"]
RUN dotnet restore "FAMS.V0.Services.SyllabusService/FAMS.V0.Services.SyllabusService.csproj"
COPY . ./FAMS.V0.Services.SyllabusService
WORKDIR "/src/FAMS.V0.Services.SyllabusService"
RUN dotnet build "./FAMS.V0.Services.SyllabusService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FAMS.V0.Services.SyllabusService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FAMS.V0.Services.SyllabusService.dll"]
