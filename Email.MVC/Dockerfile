FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 55124
EXPOSE 44378

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY ../Email.MVC/Email.MVC.csproj ../Email.MVC/
RUN dotnet restore ../Email.MVC/Email.MVC.csproj
COPY . .
WORKDIR /src/../Email.MVC
RUN dotnet build Email.MVC.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Email.MVC.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Email.MVC.dll"]
