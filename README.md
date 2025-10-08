# SwapiWrapperApi
The **SWAPI Wrapper API** is a simple RESTful API built using **ASP.NET Core** that acts as a wrapper around the [Star Wars API (SWAPI)](https://swapi.dev).  
It simplifies access to Star Wars data — such as films and characters — by providing clean, well-structured endpoints for client applications to use.
To see instructions on how to use, please scroll down...

## 🚀 Features
- Retrieve a list of **films** from the Star Wars universe.
- Retrieve **characters** for a specific film
- Retrieve **starships** for a specific film
- Built-in **Swagger UI** for interactive API documentation.
- **Rate-limiting** for api endpoints to prevent api abuse.
- **Caching** api responses to prevent redundant api calls and optimizing response times 
- **JWT Authentication** to protect endpoints.
- **CORS support** for frontend integration (e.g. Angular or any web client).
- Ready-to-use **Dockerfile** for containerized deployment.

## ⚙️ Getting Started
Make sure you have the following:
- Visual Studio to run the code
- .NET 8 SDK
- Docker (*Optional)

Setup instuctions:
1. Open visual studio
     Open up Visual Studio and go to your terminal 
2. Clone the repo
     Run the following command in your terminal: **git clone https://github.com/Cajee7/SwapiWrapperApi.git**
3. Navigate to the repo
     Go into the repo by running the following command: **cd SwapiWrapperApi**
4. Restore dependencies
     Run the following command to restore dependencies: **dotnet restore**
5. Run application
     Thereafter run the application with this command: **dotnet run**
6. Open browser (*Optional)
     If the browser does not open or it opens and doesnt show you swagger, make sure you change the url to the following: **https://localhost:7080/swagger**
7. To build and run in **Docker**
     Run the following commands: **docker build -t swapiwrapperapi .**
     Once the above command is done executing, run this one: **docker run -d -p 8080:8080 --name swapiwrapper swapiwrapperapi**
     Your application should be running on the following site: **http://localhost:8080/swagger**

## 🧩 Architecture Overview
This SWAPI Wrapper API follows a layered architecture to seperate responsibilites for clarity, scalability and maintainability.

It is structured in 3 layers:
**1. Controller layer** - Exposes endpoints and delegates logic (which in this case there is none).
**2. Service layer** - Uses RestSharp to fetch data from external Swapi Api and processes and filters the data before returning it to the controllers.
**3. Model layer** - Defines the data structures and maps the Swapi Json responses to the C# models.

## 🧠 Design Decisions
**1. Authetication** - Chose JWT for better scalability, easier integration with frontend applications and as per industry norm.
                     - Tokens are also stored in HTTP only cookies as a defense measure to prevent attacks while maintaining token persistance.

**2. Rate limiting** - Used Fixed Window Rate Limiter to allow 10 requests per minute to pretent API abuse and ensure fair resource allocation across users.
                     - Added a queue with a 1 slot capacity to handle concurrent requests better rather than immediatly rejecting them.

**3. Security** - Used symmetric key cryptography for JWT signing, providing strong security with good performance and used UTC timestamps to prevent timezone related bugs when token expires.
                - USed config based secrets, JWT settings (secret key, issuer, audience, etc. ) stored in config files rather than hardcoded following best security practices.

**4. API Documentation** - Swagger Integrations since im more familiar to it and to provide interactive API Documentation for easier testing an integration by frontend devs.
                         - Configured Swagger to automatically prepend "Bearer" prefix, improving developer experience.

**5. Caching** - Implementend in memory caching to prevent and reduce redundant api calls to Swapi, improving response times and reducing load.

**6. CORS Policy** - Costs configured specifically for 'http://localhost:4200' to prevent unauthorized cross origin requests.

**7. Containerizing with Docker** - Application containerized using Docker to ensure consistent development, testing and deployment environments across different machines and platforms.

## Instructions on how to use
Before calling any endpoint, you need to be authenticated...

Click on the try it out button at the authenticate endpoint
Fill in the fields: 
"username": "admin",
"password": "Password"
Thereafter in the response body you will see a message and a token

Copy the token and click on the green authorize button found above the authenticate endpoint. Alternatively you can click on any lock icons as well.

Clicking on the button will open a pop up with the heading available authorizations.

In the value textbox, paste your token in and click on authorize.

Congradulations, you are now authenticated. Now you can use any endpoint. 
If you get an error that says kerner bearer or a status code 401 error, please repeat the instructions from the top again.
   
