FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY DiceBound/*.csproj DiceBound/
RUN dotnet restore DiceBound/DiceBound.csproj

COPY . .
WORKDIR /src/DiceBound
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DiceBound.dll"]
