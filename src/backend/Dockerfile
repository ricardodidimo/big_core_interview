FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

COPY big_core.Api/big_core.Api.csproj big_core.Api/
COPY big_core.Common/big_core.Common.csproj big_core.Common/

RUN dotnet restore "big_core.Api/big_core.Api.csproj"

COPY . .

WORKDIR "/big_core.Api"

RUN dotnet build "big_core.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "big_core.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
EXPOSE 80
EXPOSE 403
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "big_core.Api.dll"]
