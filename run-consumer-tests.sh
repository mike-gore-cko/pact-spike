#!/bin/bash  
dotnet test Consumer.Tests

export PACT_BROKER_BASE_URL="http://localhost:9292"
export PACT_BROKER_USERNAME=""
export PACT_BROKER_PASSWORD=""

version=1.0.0
git_hash=$(git rev-parse HEAD)
git_branch=$(git rev-parse --abbrev-ref HEAD)

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
    --consumer-app-version="$version" \
    --tag="$git_hash" \
    --branch="$git_branch"