## Craftsman Contact

#### Pet project

This is a .NET 8.0 web application for connecting clients and various kinds of craftsmen. The app uses an MSSQL database and Entity Framework to deal with data. (During development, I use the official MSSQL Docker image.) The project includes Identity for user handling and JWT for authentication. Based on roles (admin and user), authorization is also set up, and the majority of the endpoints are restricted.

The unit test project uses NUnit and Moq, for integration testing, I used xUnit. Based on the tests, I have set up the GitHub Actions workflow for CI.

At this moment, I'm working on the React frontend, and I'm making experiments with React Bootstrap to make my app responsive.
