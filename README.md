This solution consists of 2 projects - WeatherFunctionApp and WeatherWebApplication each of them corresponding to a certain task

TASK 1 (WeatherFunctionApp)
Must use:
• Azure Function (Cloud/Local)
• Azure Storage (Cloud /Local storage emulator)
a. Table
b. Blob
• .Net Core 6.

Achieve:
• Every minute, fetch data from
https://api.openweathermap.org/data/2.5/weather?q=London&amp;appid=YOUR_API_KEY (you
need to sign up to generate a new API Key) and store success/failure attempt log in the table
and full payload in the blob.
• Create a GET API call to list all logs for the specific time period (from/to) (implement an
Azure Function HTTP Trigger that queries the Azure Table Storage and returns logs within
the specified time period).
• Create a GET API call to fetch a payload from blob for the specific log entry (implement an
Azure Function HTTP Trigger that retrieves the specific payload from Azure Blob Storage
based on the log entry ID).

P.s. To run it locally local.settings.json should be updated with according local connection settings. And for testing Postman, azurite and Microsoft Azure Storage Explorer can be used.

TASK 2 (WeatherWebApplication)
Must use:
• ASP.NET CORE MVC (6)
• C#
• JavaScript or Typescript (preferred)
• React (preferred)
• SQL.
Achieve:
 Using any public weather API receive data (country, city, temperature) from 2 cities in 2-3
countries - with periodical update 1/min.
 Store this data in the database and show in graphs: Min and Max temperature
(Country\City\Temperature\Last update time).

P.s. Countries and cities are hardcoded in the application. To run it locally appsettings.json should be updated with according local connection settings. Min and Max temperature are shown using simple bar graphs that contain info about each city.