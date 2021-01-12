FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["FreakyFashionServices.OrderService.Producer/FreakyFashionServices.OrderService.Producer.csproj", "./"]
COPY ["FreakyFashionServices.Shared/", "./FreakyFashionServices.Shared"]

RUN dotnet restore "FreakyFashionServices.OrderService.Producer.csproj"
COPY ./FreakyFashionServices.OrderService.Producer .
WORKDIR "/src/."
RUN dotnet build "FreakyFashionServices.OrderService.Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreakyFashionServices.OrderService.Producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreakyFashionServices.OrderService.Producer.dll"]
