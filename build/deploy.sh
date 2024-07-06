#!/bin/bash

envScript=""

if [ -z "$1" ] 
then
  envScript="env.Development"
else
  envScript=env."$1"
fi

set -a
source $envScript
rm -f docker-compose.yml
envsubst < "docker-compose.template.yml" > "docker-compose.yml"
rm -f db-creation.sql 
cp -f ../db/db-creation.template.sql .
envsubst < "db-creation.template.sql" > "db-creation.sql"
rm -f db-creation.template.sql 
set +a
docker-compose down
docker-compose up -d