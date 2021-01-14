FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FreakyFashionServices.ProductPrice/FreakyFashionServices.ProductPrice.csproj", "./"]
RUN dotnet restore "FreakyFashionServices.ProductPrice.csproj"
COPY ./FreakyFashionServices.ProductPrice .
WORKDIR "/src/."
RUN dotnet build "FreakyFashionServices.ProductPrice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreakyFashionServices.ProductPrice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreakyFashionServices.ProductPrice.dll"]
