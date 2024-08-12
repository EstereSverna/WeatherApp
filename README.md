# Weather Solution Overview

This repository contains two projects: **WeatherFunctionApp** and **WeatherWebApplication**. Each project addresses specific tasks related to weather data management.

## Project 1: WeatherFunctionApp

### Requirements
- **Azure Function** (Cloud/Local)
- **Azure Storage** (Cloud/Local Storage Emulator)
  - Table Storage
  - Blob Storage
- **.NET Core 6**

### Objectives
- Fetch weather data every minute from the [OpenWeatherMap API](https://api.openweathermap.org/data/2.5/weather?q=London&appid=YOUR_API_KEY) (You need to sign up to generate an API key).
  - Log the success or failure of each attempt in Azure Table Storage.
  - Store the full payload in Azure Blob Storage.

- **API Endpoints**:
  - **GET Logs**: Retrieve logs for a specific time period (`from/to`).
    - Implement an Azure Function HTTP Trigger to query Azure Table Storage and return logs within the specified period.
  - **GET Payload**: Fetch the payload from Blob Storage for a specific log entry.
    - Implement an Azure Function HTTP Trigger to retrieve the payload from Azure Blob Storage based on the log entry ID.

### Running Locally
- Update `local.settings.json` with the appropriate local connection settings.
- Use Postman, Azurite, and Microsoft Azure Storage Explorer for testing purposes.

## Project 2: WeatherWebApplication

### Requirements
- **ASP.NET Core MVC (6)**
- **C#**
- **JavaScript/TypeScript** (TypeScript preferred)
- **React** (preferred)
- **SQL**

### Objectives
- Fetch weather data (country, city, temperature) from two cities in 2-3 different countries, with updates every minute using any public weather API.
- Store the fetched data in a database.
- Display the data using graphs:
  - Show Minimum and Maximum temperatures.
  - The graph should display the Country, City, Temperature.

### Running Locally
- Countries and cities are hardcoded in the application.
- Update `appsettings.json` with the appropriate local connection settings.
- Min and Max temperatures are visualized using simple bar graphs that provide information about each city.

## Notes
- Ensure that the necessary dependencies are installed and configured correctly before running the projects.
- Follow the instructions in the respective project folders for setup and usage.
