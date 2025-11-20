# Free Spin Game 

A .net 10 Web API for free spin games that is built following DDD praciteces using **Clean Architecture**, **CQRS**, and **Optimistic Concurrency Control**.

This project implements a system where players can participate in different campagins. It enforces strict participation limits
per campaign and handles high-concurrency race conditions (e.g. a player clicks "Spin" from multiple devices simultaneously)
using EF Core concurrency tokens and an automatic retry mechanism. 

## Key Features

* **Clean architecture:** enforcing strict separation of concerns 
* **CQRS pattern** implemented using MediatR to separate Command and Query logic
* **Optimistic concurrency** that handles race conditions using a manual ConcurrencyKey and EF Core's built in IsConcurrencyToken
* **Resilience** is applied with the use of automatic retry logic to recover from concurrency conflicts
* **Global exception handling** with custom middleware
* **Testing** is implemented with **xUnit** and **FakeItEasy**
* **In-memory database** SQLite in-memory

## Getting Started

### Prerequisites
* You need to have [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) installed on your machine

### How to Run
1. Clone the repository
2. Navigate to the root folder 
3. Run the application:
   ```bash
   dotnet run
   ```
4. The API should launch a new tab in the browser where Swagger should be loaded
5. If it doesn't - open your browser to the Swagger UI on the following address (check the port in the console output):
   ```text
   http://localhost:5xxx/swagger/index.html
   ```
6. When you start the application, the in-memory database is automatically seeded with two campaigns. You can use these values to test the endpoints.
   ```text
   campaignId: "1", maxSpinCount: 2
   campaignId: "2", maxSpinCount: 3
   ``` 

### Running Tests
The project includes tests for the Domain logic and the Application handlers. To execute them run this command:
```bash
   dotnet run
   ```
   