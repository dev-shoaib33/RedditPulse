# RedditPulse

RedditPulse is a .NET Core console application designed to monitor a specified subreddit and track the most popular posts and users in near real-time. The application leverages Reddit's API and periodically logs statistics about the top post and the user with the most posts.

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Setup](#setup)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Testing](#testing)
- [License](#license)

## Features

- **Real-time Monitoring**: Continuously monitors a specified subreddit for new posts.
- **Statistics Tracking**: Keeps track of the top post based on upvotes and the user with the most posts.
- **Logging**: Logs statistics periodically to the console.
- **Modular Design**: Uses Dependency Injection (DI) and interfaces for easy testing and maintainability.

## Prerequisites

- [.NET 8](https://dotnet.microsoft.com/download/dotnet)
- A Reddit account with API access (you'll need to register your application to get the Client ID and Secret)

## Setup

1. **Clone the repository:**

   ```bash
   git clone https://github.com/dev-shoaib33/RedditPulse.git
   cd RedditPulse

2. **Configure Reddit API credentials:**

Update the `appsettings.json` file with your Reddit API credentials and the subreddit you want to monitor.

```json
{
  "Reddit": {
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "Subreddit": "your-subreddit",
    "BaseUrl": "https://oauth.reddit.com"
  }
}
```
3. **Configure Reddit API credentials:**

Run the following command to restore the required packages:
```bash
   dotnet restore
```

##Usage

To run the application:
```bash
   dotnet run --project RedditPulse
```

The application will start monitoring the specified subreddit, log the top post and user statistics to the console periodically.

# Project Structure - RedditPulse

The `RedditPulse` project is organized into the following directories and files:

```{r, echo=FALSE, results='asis'}
RedditPulse/
│
├── Dependencies/
│   ├── Helpers/
│   │   └── RestClientWrapper.cs    # Wrapper for RestClient
│   └── Interfaces/
│       ├── ILoggerService.cs       # Interface for logging service
│       ├── IRedditAuthService.cs   # Interface for Reddit authentication service
│       ├── IRedditService.cs       # Interface for Reddit service
│       ├── IRestClientWrapper.cs   # Interface for RestClient wrapper
│       └── IStatisticsService.cs   # Interface for statistics service
│
├── Models/
│   ├── RedditPost.cs               # Model for a Reddit post
│   ├── RedditUser.cs               # Model for a Reddit user
│   └── SubredditStatistics.cs      # Model for subreddit statistics
│
├── Services/
│   ├── AuthService/
│   │   └── RedditAuthService.cs    # Implementation of Reddit authentication service
│   ├── ConsoleLoggerService.cs     # Implementation of logging service
│   ├── RedditService.cs            # Implementation of Reddit service
│   └── StatisticsService.cs        # Implementation of statistics service
│
├── appsettings.json                # Application settings file
├── Program.cs                      # Main entry point of the application
└── Usings.cs                       # Global usings for the project
```


## Explanation

- **Dependencies**: This folder contains helper classes and interfaces that provide support for the core functionality of the application.
  - **Helpers/RestClientWrapper.cs**: A wrapper for the `RestClient` used for making API requests.
  - **Interfaces**: Contains the interfaces that define contracts for services used in the project:
    - **ILoggerService.cs**: Interface for the logging service.
    - **IRedditAuthService.cs**: Interface for the Reddit authentication service.
    - **IRedditService.cs**: Interface for the Reddit service.
    - **IRestClientWrapper.cs**: Interface for the RestClient wrapper.
    - **IStatisticsService.cs**: Interface for the statistics service.

- **Models**: This directory contains the data models used throughout the application.
  - **RedditPost.cs**: Represents a Reddit post.
  - **RedditUser.cs**: Represents a Reddit user.
  - **SubredditStatistics.cs**: Represents the statistics for a subreddit.

- **Services**: This folder contains the implementation of various services that provide the core logic of the application.
  - **AuthService/RedditAuthService.cs**: Implementation of the Reddit authentication service.
  - **ConsoleLoggerService.cs**: Implementation of the logging service.
  - **RedditService.cs**: Implementation of the Reddit service that interacts with the Reddit API.
  - **StatisticsService.cs**: Implementation of the statistics service that tracks and processes subreddit data.

- **appsettings.json**: The configuration file where application settings, such as API credentials and subreddit details, are stored.

- **Program.cs**: The main entry point of the application where services are configured and the application is started.

- **Usings.cs**: Contains global `using` directives that simplify the management of namespaces across the project.

This structure is designed to promote modularity, testability, and maintainability, making it easier to manage and extend the application over time.


**Testing**

Unit tests for the application are located in the `RedditPulse.UnitTests` project.

To run the tests, use the following command:

```bash
dotnet test
```

The tests are organized as follows:

```{r, echo=FALSE, results='asis'}
RedditPulse.UnitTests/
├── Services/
│   ├── RedditServiceTests.cs       # Unit tests for RedditService
│   └── StatisticsServiceTests.cs   # Unit tests for StatisticsService
```

**License**

This project is licensed under the MIT License - see the LICENSE file for details.
