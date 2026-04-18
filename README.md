# ProLearning API

An API built in C#/ASP.NET Core to find, get, and perform ProLearning tasks. This project mainly focuses on data and system design and aims to provide a well-structured API.

The live deployment is at `https://prolearning-production.up.railway.app`

## Usage
The main endpoint is `/recommendations`. Send a GET request to this endpoint with the following query parameters:
|Parameter|Value|
|--|--|
|interestAreas|string[]|
|skillLevels|int[] `(0 = beginner, 1 = intermediate, 2 = advanced)`|
|goals|string[]|
|educationLevel|string|
|page|int (default 1)|
|pageSize|int (default 25)|

To get possible values for `interestAreas` ,`goals`, and `educationLevel`,  send a GET request to endpoints `/interestarea`, `/goal`,  and `/educationlevel` respectively.

Each entry in skillLevels refers to the interestAreas entry with the corresponding index.

You will then get a response with the following schema:
```json
{
  "items": [
    {
      "name": "string",
      "url": "string",
      "score": 0,
      "scoreBreakdown": {
        "interestAreas": [
          {
            "interestArea": "string",
            "skillLevel": 0,
            "score": 0
          }
        ],
        "goals": [
          {
            "goal": "string",
            "score": 0
          }
        ]
      }
    }
  ],
  "page": 0,
  "pageSize": 0,
  "totalCount": 0,
  "hasNextPage": true,
  "hasPreviousPage": true
}
```
For full API documentation, see [api.md](https://github.com/WatIzDat/prolearning/blob/main/ProLearning.Api/api.md).
Note that `/learningactivity` endpoints are for admin management purposes (makes it easy to add/edit/remove learning activities)  and require an API key. To test it, set up the project locally.
## Local Setup
Make sure you have installed:

 - Docker
 - Docker Compose
 - Git
 - .NET

Clone the repository:

    git clone https://github.com/WatIzDat/prolearning.git
Inside docker-compose.yaml, set the API key:
```yaml
services:
  prolearning.api:
    image: prolearning.api
    ports:
      - "5075:8080"
    build:
      context: ProLearning.Api
      dockerfile: Dockerfile
    depends_on:
      - database
    environment:
      ApiKey: "your-api-key" # add this
      ConnectionStrings__Database: "Host=database;Port=5432;Database=default_database;Username=postgres;Password=postgres;Include Error Detail=true"
  database:
    image: postgres:latest
    ports:
      - "5433:5432"
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: default_database
    volumes:
      - ${PWD}/db-data/:/var/lib/postgresql/data/
```
Restore packages:

    dotnet restore

Start Docker services (API and database):

    docker compose up
Apply all database migrations:

    dotnet ef database update
Connect to the Postgres database in any way you'd like (psql, pgAdmin, etc.) and execute `seed_db.sql` to seed the database:

    psql -d default_database -h localhost -p 5433 -U postgres -f seed_db.sql

Finally, make a POST request to `http://localhost:5075/learningactivities?apiKey=your-api-key` with the contents of `learningActivitiesSampleData.json` as the request body to add sample data to the database.
## Incomplete / Planned Features
### Incomplete

 - **Rule engine depth**: A more complex algorithm to rank activities beyond additive score boosts, such as boosts with multiple conditions
 - **Activity coverage**: More learning activities need to be added, such as with scraping
 - **Test coverage**: Proper testing (unit tests, integration tests) should be written for a production system
 - **Activity descriptions**: Every learning activity should come with a description so users don't have to open the URL to understand every activity
 - **Searching/filtering**: A way to search/filter recommendation results 
### Planned (what I'd build with more time)
 - **Feedback loop**: Users should be able to rate or dismiss recommendations, which can be used to adjust score boosts
 - **Hybrid recommendation engine**: Using LLMs or a vector similarity search along with rule-based recommendations to improve results
 - **Human-readable explanation**: Currently returns a breakdown of all score boosts, which can be converted to a human-readable explanation
 - **Observability and ratelimiting**: Should be included in production systems
## Assumptions
 - Used small sample dataset (25 activities)
 - Activities are modeled with a name, URL, and suitable education levels
 - Ranking is rule-based, using additive score boosts based on interest areas and goals
## Stack/Tools Used
 - C#/ASP.NET Core
 - EF Core
 - PostgreSQL
 - Docker
 - Railway (for deployment)
 - Widdershins (for docs generation)
 - AI for planning and decision-making, didn't use for writing code

