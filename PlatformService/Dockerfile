# Pull down the sdk image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env 

# Pull down working directory
WORKDIR /app

#Copy the csproj file to know the dependencies to build with
COPY *.csproj ./

# pull down the packages contained in the cs proj
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out


# Build runtime version
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Create working directory
WORKDIR /app

# Copy content on what is being build
COPY --from=build-env /app/out .

# Set entry point
ENTRYPOINT ["dotnet", "PlatformService.dll"]