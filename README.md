## Shipments - Demo
This project is a very basic Demo for NGroot. To Run it, make sure to have SQL Server Installed and  Then run:

```bash
dotnet restore
dotnet tool install -g ef
dotnet ef migrations add Initial
dotnet ef database update
``` 

After that you should be ready to go!