FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 53591
EXPOSE 44304

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Email.API/Email.API.csproj Email.API/
RUN dotnet restore Email.API/Email.API.csproj
COPY . .
WORKDIR /src/Email.API
RUN dotnet build Email.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Email.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Email.API.dll"]