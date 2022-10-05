dotnet test

export PACT_BROKER_BASE_URL="http://localhost:9292"
export PACT_BROKER_USERNAME=""
export PACT_BROKER_PASSWORD=""

docker run --rm \
    -w ${PWD} \
    -v ${PWD}:${PWD} \
    -e PACT_BROKER_BASE_URL \
    -e PACT_BROKER_USERNAME \
    -e PACT_BROKER_PASSWORD \
    --network="host" \
    pactfoundation/pact-cli:latest \
    publish \
    ${PWD}/Consumer.Tests/pacts \
    --consumer-app-version 1.0.0 \