# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the solution and the project files
COPY LibBMS/*.csproj ./LibBMS/
COPY LibBMS.Tests/*.csproj ./LibBMS.Tests/

# Restore the NuGet packages
RUN dotnet restore LibBMS/LibBMS.csproj
RUN dotnet restore LibBMS.Tests/LibBMS.Tests.csproj

# Copy the entire source code into the container
COPY . .

# Build the console app and the test project
RUN dotnet build LibBMS/LibBMS.csproj -c Release
RUN dotnet build LibBMS.Tests/LibBMS.Tests.csproj -c Release

# Run the tests using the dotnet test command
RUN dotnet test LibBMS.Tests/LibBMS.Tests.csproj