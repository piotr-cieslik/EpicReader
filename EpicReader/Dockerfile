FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["EpicReader/EpicReader.csproj", "EpicReader/"]
RUN dotnet restore "EpicReader/EpicReader.csproj"
COPY . .
WORKDIR "/src/EpicReader"
RUN dotnet build "EpicReader.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EpicReader.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EpicReader.dll"]