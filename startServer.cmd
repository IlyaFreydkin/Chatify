docker rm -f mariadb_chatify 2> nul
docker run --name mariadb_chatify -d -p 13307:3306 -e MARIADB_USER=root -e 
MARIADB_ROOT_PASSWORD=password 
mariadb:10.10.2
dotnet build ChatifyProject/ChatifyProject.Webapi --no-incremental --force
dotnet run -c Debug --project ChatifyProject/ChatifyProject.Webapi
