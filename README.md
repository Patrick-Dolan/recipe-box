# Recipe Box

#### By _**Christopher Nakayama and Patrick Dolan**_

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
* Click on the Administration tab in the Navigator on the left side of the screen. (this tab will likely be on the bottom of the window)
* In the management section of this administration tab click on the button called <code>Data Import/Export</code>. 
* Once the data import window opens click on <code>Import from Self-Contained file</code> radio button, navigate into the project folder and select <code>patrick_dolan.sql</code> using the file path next to that radio button.
* Now click on the <code>New...</code> button in the section marked "Default Schema to be Imported To" directly underneath the import options section.
* Click on the "Import Progress" tab at the top of the Data import window.
* At the bottom of this tab click the button that reads <code>Start Import</code>.
* Confirm the database has been imported and you can check it by clicking the "Schemas" tab on the navigator at the left side of the program. Right click in the white space and select "Refresh All"
* Now using your IDE navigate into the HairSalon/HairSalon/ folder and use the command <code>dotnet run</code> to launch the program. 
* The site should be available at the server address you used in the <code>appsettings.json</code> folder.
* Make sure to change the Identity services configuration to make passwords more secure as the current settings are in place to make development easier but are not good settings for secure passwords

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