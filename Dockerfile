﻿FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["src/OzonEdu.MerchendiseService.Presentation/OzonEdu.MerchendiseService.Presentation.csproj", "src/OzonEdu.MerchendiseService.Presentation/"]
RUN dotnet restore "src/OzonEdu.MerchendiseService.Presentation/OzonEdu.MerchendiseService.Presentation.csproj"

COPY . .

WORKDIR "/src/src/OzonEdu.MerchendiseService.Presentation"

RUN dotnet build "OzonEdu.MerchendiseService.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OzonEdu.MerchendiseService.Presentation.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM runtime AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OzonEdu.MerchendiseService.Presentation.dll"]