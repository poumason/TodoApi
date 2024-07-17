# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ./todoapiapp/ ./todoapiapp
RUN ls ./todoapiapp/
RUN dotnet restore


WORKDIR /source/todoapiapp
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
RUN ls /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "TodoApi.dll"]