# Todo App

This is a simple todo app built with dotnet 6.

## Architecture

This app is built with a simple architecture with 3 layers.

![ToDo App Arch](docs/image01.png "ToDo App Arch")

The code is organized in 2 projects:

- TodoApi: This is the web api project. It contains the controllers and the models.
- TodoWebapp: This is the frontend project. It contains the views and the client code.

Todo data is stored in a PostgreSQL database.

## Running the app using containers

The app can be run using docker-compose. The docker-compose file is located in the root of the project.

Run following command to start the app:

```bash
docker-compose up -d
```

The app will be available at <http://localhost:8080>
