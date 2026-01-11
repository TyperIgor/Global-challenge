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

In your local machine, with docker installed run the following command to initiate database.
```docker 
docker run --name postgresdb -p 5432:5432 -e POSTGRES_USER=postgres -e POSTGRES_DB=whateversecret -d postgres
```
Use following command to validate container is running.
```docker
docker container ps
```

After you opened application at 'appsettings.json', you will find a connection string. Of course if you are running locally just change there.

```json
"ConnectionsStrings": { "Postgres": "Host=localhost;Username=postgres;Password=whaeversecret;Port=5432;Database=postgres;"}
```

## ☕ How to Interact with the API

| Method | Endpoint            | Description               |
| ------ | ------------------- | ------------------------- |
| GET    | `/devices`      | Get all devices           |
| GET    | `/devices/{id}` | Get device by ID          |
| POST   | `/devices`      | Create a device           |
| PATCH  | `/devices/{id}` | Partially or fully update a device |
| DELETE | `/devices/{id}` | Delete a device           |




## Technologies

* 

<h2>⚡ Bonus </h2>
This application is also available in a temporarly production environment, where you can test immediately and its configured also with Postgres instance from Render cloud host platform.

[device-api-host](https://global-challenge.onrender.com/swagger")

### Additional Information 






