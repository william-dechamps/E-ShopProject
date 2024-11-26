#!/bin/bash

docker run -d --name EShopProject -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=+-@william-dechamps@-+ -e POSTGRES_DB=EShopProject -v $(pwd)/db_data:/var/lib/postgresql/data -p 5433:5432 postgres:17-alpine