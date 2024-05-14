# pingpong-leaderboard
## How to run
### 1. Build docker image
- run the command: ``docker build -t pingpong-api -f ./Dockerfile .``
### 2. Run the docker image
- run the container: ``docker run -d -p 80:8080 pingpong-api``

### 3. Access the website
go to *[localhost:80/index.html](localhost:80/index.html )* to access the scoreboard

# Notes
The database is created with SQLite3 and if it's not deployed with a volume mount, the database will be ephemeral, meaning that the data will only be persisted in the lifetime of the container.