####################
# Test Container
####################
# Setup the dotnet environment
FROM mcr.microsoft.com/dotnet/sdk:6.0

####################
# Deploy Container
####################
# Setup the dotnet environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
COPY src/SlackBot /app/src/slackbot
WORKDIR /app/src/slackbot
RUN dotnet build -c Release

# Publish the code for deploy as an image
# Only for local runs or to deploy as cloud run
FROM build-env AS publish
WORKDIR /app/src/slackbot
RUN dotnet publish -c Release -o out --no-build

# Minimal container for deployed code
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as deploy
WORKDIR /app
EXPOSE 8080
ENV TZ="Africa/Johannesburg"
COPY --from=publish app/src/slackbot/out .
ENTRYPOINT dotnet SlackBot.dll
