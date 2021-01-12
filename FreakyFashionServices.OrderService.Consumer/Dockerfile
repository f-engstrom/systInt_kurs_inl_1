FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["FreakyFashionServices.OrderService.Consumer/FreakyFashionServices.OrderService.Consumer.csproj", "./"]
COPY ["FreakyFashionServices.Shared/", "./FreakyFashionServices.Shared"]

RUN dotnet restore "FreakyFashionServices.OrderService.Consumer.csproj"
COPY ./FreakyFashionServices.OrderService.Consumer .
WORKDIR "/src/."
RUN dotnet build "FreakyFashionServices.OrderService.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreakyFashionServices.OrderService.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreakyFashionServices.OrderService.Consumer.dll"]