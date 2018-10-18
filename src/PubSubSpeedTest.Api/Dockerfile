FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/PubSubSpeedTest.Api/*.csproj ./src/PubSubSpeedTest.Api/
RUN dotnet restore

# copy everything else and build app
COPY src/PubSubSpeedTest.Api/. ./src/PubSubSpeedTest.Api/
WORKDIR /app/src/PubSubSpeedTest.Api
RUN dotnet publish -c Release -o out -r linux-x64

FROM microsoft/dotnet:2.1-runtime-deps AS runtime
WORKDIR /app
COPY --from=build /app/src/PubSubSpeedTest.Api/out ./
ENTRYPOINT ["./PubSubSpeedTest.Api"]
