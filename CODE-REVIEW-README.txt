PPOk Pharmacy System Version 0.3 04/19/17

GENERAL USAGE NOTES:
____________________

- Default Admin uses email of "oliviertyishime@gmail.com" and password "Test_1" as credintials.

- Currently requires an existing database named "ppok_refill_system".

- Once database is written, the template table must be manually loaded.

- If some of the pages take a while to load, please be patient and allow the process to finish. DO NOT REFRESH THE PAGE PLZ!!!


CODE STRUCTURE:
_______________

- AspNet.Identity.Dapper is the project that handles the microsoft identity framework. 
  Our version was modified to use dapper instead of Microsoft Entity Framework.
  In our version, the member table (IdentityUser) represents all human entities (patients, pharmacists, and admins).

- Database.Member is the project that creates the database. 
  However, you will have to load the data manually after the database is created.

- ppok_refill.Models manages our custom objects including database management objects.

- Web is the project that runs the rest of the program.
  ViewModels are found in the "Models" folder.


Created by team Annonymous:
___________________________

Jayla Luxton
Jesus Arredondo
Olivier Tuyishime
Jean Paul Iradukunda
Augustine Nshimiyiman
Keenan Gates