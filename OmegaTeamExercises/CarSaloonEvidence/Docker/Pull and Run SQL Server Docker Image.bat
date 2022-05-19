rem 1. Pull the SQL Server 2019 Linux container image from Docker Hub.
call docker pull mcr.microsoft.com/mssql/server:2019-CU9-ubuntu-16.04

rem 2. To run the container image with Docker, you can use the following command from a bash shell (Linux/macOS) or elevated PowerShell command prompt.
call docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=C@r$@loonEvidence-2021!" -p 1433:1433 --name CarSaloon2021 -d mcr.microsoft.com/mssql/server:2019-CU9-ubuntu-16.04

rem Use the docker exec -it command to start an interactive bash shell inside your running container. In the following example sql1 is name specified by the --name parameter when you created the container.
docker exec -it CarSaloon2021 "bash"

rem Once inside the container, connect locally with sqlcmd. Sqlcmd is not in the path by default, so you have to specify the full path.
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "C@r$@loonEvidence-2021!"