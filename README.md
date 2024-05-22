# BattAnimeZone (Work in Progress)

### Blazor Web App (Webserver Only) written in C# with .NET8

This is a project I am working on in order to learn Blazor.
For now it is only capable for searching for animes, and display information on them as well as on studios.
The program uses string-similarity comparsion to find similar animes for the <br> searched-string so there's no need for an exact search term.

- The data was fetched from MAL using the Jikan API to build the database.
- Missing numerical data was filled with 0.
- You can find the python script for it in the /Files folder.
- All the data the program runs on is in the /Files folder.
- The frontend was made by using [Radzen](https://blazor.radzen.com) components.
- Currently the entire database is in memory. You can change the file it uses in the AnimeService's constructor to decrease memory consumption if needed.
- The program is currently a web server, but the data handling was written in a way <br> that makes it easy to convert it into a combined Web server and WASM application. <br> Only an API controller is needed between AnimeService and the pages. (and maybe DTO's to decrease the internet traffic)
	- Current dataflow: Database (in-memory) <-> AnimeService <-> Razor pages.  
	- Required dataflow for WS + WASM: Database <-> AnimeService <-> <b>Controller<b> <-> Razor pages (front end).
	- This causes the program to use more RAM than it would if it were just a web server because of some data duplication.


### Run

- .NET8 Required
- Download repository
- Open the .sln
- Run


