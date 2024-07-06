#!/bin/bash

envScript=""

if [ -z "$1" ] 
then
  envScript="env.Development.sh"
else
  envScript=env."$1".sh
fi

source $envScript
rm -f docker-compose.yml
envsubst < "docker-compose.template.yml" > "docker-compose.yml"
docker-compose down
docker-compose up -d