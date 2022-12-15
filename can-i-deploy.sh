#!/bin/bash  
#dotnet test Consumer.Tests
#
export PACT_BROKER_BASE_URL="http://localhost:9292"
export PACT_BROKER_USERNAME=""
export PACT_BROKER_PASSWORD=""
#
#version=1.0.0
#git_hash=$(git rev-parse HEAD)
#git_branch=$(git rev-parse --abbrev-ref HEAD)

# See https://docs.pact.io/pact_broker/can_i_deploy
docker run --rm \
    -w ${PWD} \
    -v ${PWD}:${PWD} \
    -e PACT_BROKER_BASE_URL \
    -e PACT_BROKER_USERNAME \
    -e PACT_BROKER_PASSWORD \
    --network="host" \
    pactfoundation/pact-cli:latest \
    pact-broker \
    can-i-deploy \
    --pacticipant "Consumer" \
    --version "1.0.0" \
    --to-environment production