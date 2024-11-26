# E-Shop Project

### Build the project without actually running it
```
dotnet build
```

### Run the project using dotnet build if necessary
```
dotnet run
```

### Database with Docker

To start the local database, you need to use the `launch_db.sh` script.
Next, you need to apply the database migrations. To do this, in the EShopProject folder, run the following command:

`dotnet ef database update`

### Fill the tables with the script
At the project root, move to the datas_test folder and execute the script with :
```
./load_datas.sh all
```

### Run the integration tests

Go to **EShopProject.Tests** folder and execute the integration tests for the project, use the following command:
```
dotnet test
```