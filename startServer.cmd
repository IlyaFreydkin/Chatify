@echo off
docker rm -f chatify_sqlserver 2> nul
docker run -d -p 1433:1433 --name chatify_sqlserver -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SqlServer2019" mcr.microsoft.com/azure-sql-edge
IF ERRORLEVEL 1 GOTO error
dotnet build ChatifyProject\ChatifyProject.Webapi --no-incremental --force
dotnet watch run -c Debug --project ChatifyProject\ChatifyProject.Webapi
GOTO end

:error
PAUSE
:end
