# Recipe Box

#### By _**Chris Nakayama and Patrick Dolan**_

#### _A web application that lets users keep track of recipes._

## Technologies Used

* C#
* .NET 5.0
* dotnet
* MySql/Workbench

## Description

A web application that lets users keep track of recipes.

## Setup/Installation Requirements

* Make sure you have MySql Workbench installed on your computer.
* Make sure to have dotnet-ef installed too.<br>
<em>This project uses <code>dotnet-ef --version 3.0.0</code> which I have globally installed but you can install it however you want. 
* Download repo to your computer using either clone or the download link.
* Open the project in VScode or your terminal/IDE of choice.
* Create a <code>appsettings.json</code> file in the root directory of the project folder. And add the following code replacing anything in square brackets with the information it represents specific to the project database:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[DATABASE-NAME-HERE];uid=[USER-ID-HERE];pwd=[YOUR-PASSWORD-HERE];"
  }
}

```

Example of complete appsettings.json:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=to_do_list;uid=root;pwd=MySuperStrongPassword;"
  }
}

```
* Make sure to run your mysql server and open MySql workbench.
* Open MySql Workbench and login to your server.
* From your terminal navigate to the <code>Factory/</code> folder and run the command <code>dotnet ef database update</code> to create the database from migrations.
* Now using your IDE navigate into the Factory.Solution/Factory folder and use the command dotnet run to launch the program.
* The site should be available at the server address you used in the appsettings.json folder.

### Test Setup/Installation

* Open the repo on your editor of choice/terminal
* Navigate to RecipeBox.Tests directory in your terminal
* Run the following command to setup testing:  
<code>dotnet restore</code>  
* Run tests by going to the test project in the terminal (RecipeBox.Solution/RecipeBox.Tests) and running the following command:  
<code>dotnet test</code>  

## Known Bugs

* _No known issues_

## Contact Me

Let me know if you run into any issues or have questions, ideas or concerns:  
dolanp1992@gmail.com

## License

_MIT_

Copyright (c) _2022_ _Christopher Nakayama and Patrick Dolan_