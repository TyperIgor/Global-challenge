# Device API - Global Challenge

![GitHub repo size](https://img.shields.io/github/repo-size/TyperIgor/README-template?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/TyperIgor/README-template?style=for-the-badge)
![GitHub forks](https://img.shields.io/github/forks/TyperIgor/README-template?style=for-the-badge)

>  Api that operates CRUD operations related to device. 

## 💻 Preconditions

Before start, make sure you have met the following requirements:

* You have instaled .Net version (9.0 or higher)
* Your operational system should be Windows / Linux / Mac.
* You must have docker installed in your machine, to validate database user cases. 

## 🚀 Getting Start <DeviceAPI>

1 - In your local machine, with docker installed run the following command to initiate database.
```docker 
docker run --name postgresdb -p 5432:5432 -e POSTGRES_USER=postgres -e POSTGRES_DB=whateversecret -d postgres
```
1.1 - Use following command to validate container is running.
```docker
docker container ps
```

2 - After you opened application at 'appsettings.json', you will find a connection string. Of course if you are running locally just change there based in the password you defined on docker command.

```json
"ConnectionsStrings": { "Postgres": "Host=localhost;Username=postgres;Password=whaeversecret;Port=5432;Database=postgres;"}
```
3 - With everything set up locally, open "Package Manage Console" and set default project "Device.API.Infrasctrure.Data" and run this following command.

```
Update-Database
```
This will apply EF migration at current database configured locally. 

# 🚀 Device API

A RESTful Web API for managing devices, built with .NET, following Clean Architecture principles.

## 📌 Features

* CRUD operations for Devices

* Swagger

* PostgreSQL database

* Docker support

* Clean Architecture (Domain, Application, Infrastructure, API)

## Endpoints

| Method | Endpoint            | Description               |
| ------ | ------------------- | ------------------------- |
| GET    | `/devices/all`      | Get all devices           |
| GET    | `/devices/{id}` | Get device by ID          |
| GET    | `/devices/all-by-brand` | Get all devices by brand          |
| GET    | `/devices/all-by-state` | Get all devices by state          |
| POST   | `/devices/add`      | Create a device           |
| PUT  | `/devices/update-device` | Partially or fully update a device |
| DELETE | `/devices/{id}` | Delete a device           |

#### Response body

- GET /devices/all

```json
{
  "data": [
    {
      "id": "60b74bdd-1399-40e0-ab19-053cb0e05a52",
      "name": "test1",
      "brand": "amex",
      "state": "in-use",
      "creationTime": "09/01/2026 14:23:49"
    }
  ],
  "message": "Device List retrieved successfully."
}
```



## 🛠 Tech Stack

.NET 9 / C# 13

ASP.NET Core Web API

Entity Framework Core

PostgreSQL

Docker 

Swagger

## ❌ Error Handling

The API uses a global exception middleware and returns standardized error responses:

```json
{
  "Title": "ValidationError",
  "message": "Device name is required"
  "Instance": "/class/Device/DeviceImp.cs/"
}
```

<h2>⚡ Bonus </h2>
This application is also available in a temporarly production environment, where you can test immediately and its configured also with Postgres instance from Render cloud host platform.

Render does not support .NET integration, but supports docker container. So an application dockerfile was created to enable implementation.   

[device-api-host](https://global-challenge.onrender.com/swagger")


👤 Author

Igor Lima
Backend Developer – .NET / C#






