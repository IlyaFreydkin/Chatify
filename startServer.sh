docker rm -f chatify_sqlserver &> nul
docker run -d -p 1433:1433 --name chatify_sqlserver -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SqlServer2019" mcr.microsoft.com/azure-sql-edge
dotnet build ChatifyProject\ChatifyProject.Webapi --no-incremental --force
dotnet watch run -c Debug --project ChatifyProject\ChatifyProject.Webapi
