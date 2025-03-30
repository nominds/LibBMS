# LibBMS (Library Book Management System)
LibBMS, is a C# .NET utility, implemented as console based application to manage the books in a library.
Application supports CRUD (create, read, update and delete) operations. The data is persisted in a in-memory data structure for simplicity.

Implementation:

* Program.cs: Main class

* LibBMS:
    ** Common: Contains utility classes and validators.
    ** Logger: Common logging class
    ** Model: Data structure of the book entity
    ** Repository: Repository interfaces and the classes
    ** Service: Main service class that interacts with the repository class
        *** Workers: Add, Update, View and Delete service worker classes

* LibBMS.Tests
    ** Testing scripts        


Build:
 dotnet build

Test:
 cd LibBMS.Tests
 dotnet test


