#!/bin/bash

envScript=""

if [ -z "$1" ] 
then
  envScript="env.Development"
else
  envScript=env."$1"
fi

set -a
export APP_UID=$((1 + $RANDOM % 9999))

source $envScript

rm -f docker-compose.yml
envsubst < "docker-compose.template.yml" > "docker-compose.yml"

rm -f db-creation.sql 
cp -f ../db/db-creation.template.sql .
envsubst < "db-creation.template.sql" > "db-creation.sql"
rm -f db-creation.template.sql

rm -f Dockerfile
envsubst < "Dockerfile.template" > "Dockerfile"
 
set +a

docker-compose down
docker-compose up

rm -f db-creation.sql
rm -f Dockerfile
rm -f docker-compose.yml