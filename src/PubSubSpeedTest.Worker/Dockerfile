FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/PubSubSpeedTest.Worker/*.csproj ./src/PubSubSpeedTest.Worker/
RUN dotnet restore

# copy everything else and build app
COPY src/PubSubSpeedTest.Worker/. ./src/PubSubSpeedTest.Worker/
WORKDIR /app/src/PubSubSpeedTest.Worker
RUN dotnet publish -c Release -o out -r linux-x64

FROM microsoft/dotnet:2.1-runtime-deps AS runtime
WORKDIR /app
COPY --from=build /app/src/PubSubSpeedTest.Worker/out ./
ENTRYPOINT ["./PubSubSpeedTest.Worker"]
