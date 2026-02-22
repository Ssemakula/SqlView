## Aim of this project ##
This project was written out of a need to have a simple way to view results of SQL commands in a grid.
It was born out of my frustration with SQL Server Management Studio (SSMS) and Valentino on how they save results of SQL commands in a grid. 
I wanted something that would allow me to quickly view results without having to save them to a file or copy and paste them into Excel,
and yet also allow me to easily export them to Excel if I wanted to.

SSMS and Valentino both have their own ways of saving results, but they fail with text that looks like numbers, and dates. 
They also fail with large text fields, and they don't allow you to easily export to Excel without going through a few steps.

## How to use this project ##
To use this project, simply clone the repository and open the solution in Visual Studio.

You can then build the solution and run the project. 
When you run the project, you will see a simple form with a text box where you can enter your SQL command, and a button to execute it. 
When you click the refresh button, the results of your SQL command will be displayed in a grid below.

Click on the load button to load a SQL command from a file.

Click the Excel button to allow you to save to an excel file (this doesn't require Excel to be installed on your machine, 
it uses a library to create the Excel file), or to open it directly in Excel if you have it installed.
