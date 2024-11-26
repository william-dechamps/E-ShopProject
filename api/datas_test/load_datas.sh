#!/bin/bash

if [ ! -f db_context.sh ]
then
	echo "no db_context.sh found. Create a file db_context.sh file with the following content :"
	echo ""
	echo "#!/bin/bash"
	echo ""
	echo ""
	echo export DB_USER=postgres
	echo export DB_PASSWORD=+-@william-dechamps@-+
	echo export DB_HOST=localhost
	echo export DB_NAME=EShopProject
	echo export DB_PORT=5432
	exit 1
fi

. db_context.sh

if [ $# -eq 0 ]; then
    echo "No past arguments"
    echo "Possible arguments : 'all' or the name of a data file (ex: datas/products_datas.sql)"
    exit 1
fi

if [ $1 == "all" ]
then
	echo "Clean db"
	PGPASSWORD=$DB_PASSWORD psql -U $DB_USER -h $DB_HOST -p $DB_PORT $DB_NAME -f datas/clean_db.sql
	echo "Load products"
	PGPASSWORD=$DB_PASSWORD psql -U $DB_USER -h $DB_HOST -p $DB_PORT $DB_NAME -f datas/products_datas.sql
else
	echo "Load $1"
	PGPASSWORD=$DB_PASSWORD psql -U $DB_USER -h $DB_HOST -p $DB_PORT $DB_NAME -f $1
fi
