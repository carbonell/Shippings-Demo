## Shipments - Demo
A little Demo for NGroot. To Run this project, make sure to have SQL Server Installed. And  Then run:

```bash
dotnet restore
dotnet tool install -g ef
dotnet ef migrations add Initial
dotnet ef database update
``` 

After that you should be ready to go!