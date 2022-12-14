#!/bin/bash
export PACT_BROKER_BASE_URL="http://localhost:9292"
export PACT_BROKER_USERNAME=""
export PACT_BROKER_PASSWORD=""

version=1.0.0
environment=production
consumer=Consumer

# See https://docs.pact.io/pact_broker/can_i_deploy
# Validate whether we can deploy the consumer to production
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
    --pacticipant "$consumer" \
    --version "$version" \
    --to-environment "$environment"