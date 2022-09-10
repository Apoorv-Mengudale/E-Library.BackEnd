#build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./WebAPI/WebAPI.csproj" --disable-parallel
RUN dotnet publish "./WebAPI/WebAPI.csproj" -c release -o /app -- no-restore

#serve stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS runtime
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
EXPOSE 443
CMD ["dotnet", "WebAPI.dll"]