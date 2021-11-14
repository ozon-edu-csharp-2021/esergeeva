#!/bin/bash

set -e
run_cmd="dotnet OzonEdu.MerchendiseService.Presentation.dll --no-build -v d"

dotnet OzonEdu.MerchendiseService.Migrator.dll --no-build -v d -- --dryrun

dotnet OzonEdu.MerchendiseService.Migrator.dll --no-build -v d

>&2 echo "MerchendiseService DB Migrations complete, starting app."
>&2 echo "Run MerchendiseService: $run_cmd"
exec $run_cmd