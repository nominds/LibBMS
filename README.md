# LibBMS (Library Book Management System)
LibBMS, is a C# .NET utility, implemented as console based application to manage the books in a library.
Application supports CRUD (create, read, update and delete) operations. The data is persisted in an in-memory data structure for simplicity.

Implementation:

* Program.cs: The main class

* LibBMS:
    * Common: Contains utility classes and validators.
    * Logger: Common logging class
    * Model: Data structure of the book entity
    * Repository: Repository interfaces and the classes
    * Service: Main service class that interacts with the repository class
        * Workers: Add, Update, View and Delete service worker classes

* LibBMS.Tests
    * Test scripts        


* Build:
    * cd ./LibBMS and execute `dotnet build`

* Test:
    * cd ./LibBMS.Tests
    * `dotnet test`

* Docker Support
    * dockerfile : can be found in the root directory.

* Building docker image to run the test project
Note: Ensure docker daemon is running and in stable state on the machine where you are building the test project.

1. Execute following commands:
    * `docker build -t libbms .`
2. After image has been built, run following to run the container    
    * `docker run --rm libbms`

Above will execute the tests inside the Docker container. If the container exit successfully the container will exit.



