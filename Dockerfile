FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS build-env
WORKDIR /app

COPY ["WebAPI/*.csproj", "WebAPI/"]
COPY ["Infrastructure/*.csproj", "Infrastructure/"]
COPY ["Domain/*.csproj", "Domain/"]
COPY ["Application/*.csproj", "Application/"]
RUN dotnet restore "WebAPI/*.csproj"

COPY . .
WORKDIR "/app/WebAPI"
RUN dotnet publish "WebAPI.csproj" -c Release -o out 

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebAPI.dll"]