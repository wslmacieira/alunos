FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["alunos.csproj", "./"]
RUN dotnet restore "./alunos.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "alunos.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "alunos.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "alunos.dll"]