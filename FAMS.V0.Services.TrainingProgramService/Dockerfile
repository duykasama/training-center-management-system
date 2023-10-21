FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FAMS.V0.Services.TrainingProgramService/FAMS.V0.Services.TrainingProgramService.csproj", "FAMS.V0.Services.TrainingProgramService/"]
RUN dotnet restore "FAMS.V0.Services.TrainingProgramService/FAMS.V0.Services.TrainingProgramService.csproj"
COPY . .
WORKDIR "/src/FAMS.V0.Services.TrainingProgramService"
RUN dotnet build "FAMS.V0.Services.TrainingProgramService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FAMS.V0.Services.TrainingProgramService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FAMS.V0.Services.TrainingProgramService.dll"]
