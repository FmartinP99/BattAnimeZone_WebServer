# BattAnimeZone (Work in Progress)

### Blazor Web App (Webserver Only) written in C# with .NET8

This is a project I am working on in order to learn Blazor.
For now it is only capable for searching for animes, and display information on them.
The program uses string-similarity comparsion to find similar animes for the <br> searched-string so there's no need for an exact search term.

- The data was fetched from MAL using the Jikan API to build the database.
- Missing numerical data was filled with 0.
- You can find the python script for it in the /Files folder.
- All the data the program runs on is in the /Files folder.
- The frontend was made by using [Radzen](https://blazor.radzen.com) components. 



### Run

- .NET8 Required
- Download repository
- Open the .sln
- Run