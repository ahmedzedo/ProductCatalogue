#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Presentation.WebAPI/Presentation.WebAPI.csproj", "Presentation.WebAPI/"]
COPY ["Configuration.WebAPI/Configuration.WebAPI.csproj", "Configuration.WebAPI/"]
COPY ["Persistance/Persistance.EF/Persistence.EF.csproj", "Persistance/Persistance.EF/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Core/Application/Application.csproj", "Core/Application/"]
COPY ["Core/Domain/Domain.csproj", "Core/Domain/"]
COPY ["Common.Extension/Common.Extension.csproj", "Common.Extension/"]
RUN dotnet restore "Presentation.WebAPI/Presentation.WebAPI.csproj"
COPY . .
WORKDIR "/src/Presentation.WebAPI"
RUN dotnet build "Presentation.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.WebAPI.dll"]