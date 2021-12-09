# BonesAPI - Getting started

## Installing development software

- Install Visual studio code from [here](https://code.visualstudio.com/download).
- Install the C# extention for VS-Code:
    - From [here](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
    - Or from the VS-Code -> Extentions (left panel) -> search -> C#
- Install Dotnet Core 6 SDK from [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) (Download the SDK not the runtime)
- (OPTIONAL) Install POSTMan - The API testing software from [here](https://www.postman.com/downloads/)
- (OPTIONAL) Install DB Browser for Sqlite from [here](https://sqlitebrowser.org/dl/) 
    - This software allows you to view the tables of the database, manually edit the entries of in the tables, create new tables, etc.
    - It also allows you to execute SQL statements directly on the database.
    - It's pretty much a lightweight version of a DBMS (Database management software). 

## Setting up environment

- The development environemnt is already setup correctly for VS-code. 
- Open the BonesAPI folder in VS-code. Make it your workspace folder, you won't be able to run the project otherwise.
- You only need to install the packages required by the project using the following command
```bash
dotnet restore
```
- Make sure the current directory is the BonesAPI folder.
- Press `CTRL + F5` to run the server.

## Testing/using the API
- You can use Swagger to view all available endpoints and test them without the need of POSTMan.
    - Go to `https://localhost:7299/swagger/index.html`
- To view all entries, you can:
    - Use Swagger.
    - From browser: Open your browser and go to `https://localhost:7299/api/BoneImage`
    - From POSTMan: Send a GET request to `https://localhost:7299/api/BoneImage`
- To view a specific entry using the ID, you can:
    - Use Swagger
    - From browser: open your browser and go to `https://localhost:7299/api/BoneImage/idNumber`
    - From POSTMan: GET request to `https://localhost:7299/api/BoneImage/idNumber`