FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src
COPY Scenario.sln ./
COPY Scenario.Web/. ./Scenario.Web/
COPY Scenario/. ./Scenario/
RUN dotnet restore -nowarn:msb3202,nu1503 /p:RestoreUseSkipNonexistentTargets="false"


WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5001
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Scenario.Web.dll"]