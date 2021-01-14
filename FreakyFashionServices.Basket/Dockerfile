FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FreakyFashionServices.Basket/FreakyFashionServices.Basket.csproj", "./"]
RUN dotnet restore "FreakyFashionServices.Basket.csproj"
COPY ./FreakyFashionServices.Basket .
WORKDIR "/src/."
RUN dotnet build "FreakyFashionServices.Basket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreakyFashionServices.Basket.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreakyFashionServices.Basket.dll"]
