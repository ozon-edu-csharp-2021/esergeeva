FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["src/OzonEdu.MerchendiseService/OzonEdu.MerchendiseService.csproj", "src/OzonEdu.MerchendiseService/"]
RUN dotnet restore "src/OzonEdu.MerchendiseService/OzonEdu.MerchendiseService.csproj"

COPY . .

WORKDIR "/src/src/OzonEdu.MerchendiseService"

RUN dotnet build "OzonEdu.MerchendiseService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OzonEdu.MerchendiseService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM runtime AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OzonEdu.MerchendiseService.dll"]