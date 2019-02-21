## About
- Examples of frequently used website functionality done in ASP.Net Core 2.1 and Angular 6.1

## Development dependencies used
### Microsoft Visual Studio 2017
	 Extensions:
		- Typewriter
		- If you plan to use Entity Framework then take a look at EF Core Power Tools (this extension can be used to reverse engineer DB tables with DB context)
### Microsoft Visual Studio Code
### ASP.Net Core 2.1.1
### Node 8.11.4 
### Angular CLI 6.1.5

## Setting up cloned project
### Check if baseUrl defined in environment object located in ~/ClientApp/src/environments/environment.local.ts 
is identical to your API URL
### Schema Compare -> Update your local / remote database with tables found in DB project

## Development
### API 
- Use Visual Studio to build/debug API
### Client 
- Use Visual Studio Code's terminal to compile client side code -> 'ng serve -c local --open' (while in Client Apps root folder)


## Creating similar project from scrach
### API
- Create new empty Asp.Net Core 2.1. Web Project
- Add middleware defined in Startup.cs based on your needs
### Client 
- CMD -> In website's root folder use: 'ng new "AppName"' (with additional options based on your needs - https://github.com/angular/angular-cli/wiki/new )
- Add ~/src/environment/environment.local.ts and environment object that contains baseUrl to your API
- Update environment.prod.ts with contents found in this project
- Update angular.json -> Add "local" property to projects.[projectName].architect.build.configurations and to projects.[projectName].architect.serve.configurations
- Add BASE_URL provider to main.ts


