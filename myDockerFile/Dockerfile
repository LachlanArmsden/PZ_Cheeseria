# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build the React frontend
FROM node:16-alpine AS client-builder
WORKDIR /app

# Copy package.json and package-lock.json
COPY pz_cheeseria.client/package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the React app source code
COPY pz_cheeseria.client/ ./

# Build the React app
RUN npm run build


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["pz_cheeseria.client/nuget.config", "pz_cheeseria.client/"]
COPY ["PZ_Cheeseria.Server/PZ_Cheeseria.Server.csproj", "PZ_Cheeseria.Server/"]
COPY ["pz_cheeseria.client/pz_cheeseria.client.esproj", "pz_cheeseria.client/"]
RUN dotnet restore "./PZ_Cheeseria.Server/PZ_Cheeseria.Server.csproj"
COPY . .
WORKDIR "/src/PZ_Cheeseria.Server"
RUN dotnet build "./PZ_Cheeseria.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PZ_Cheeseria.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PZ_Cheeseria.Server.dll"]