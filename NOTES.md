# Notes

Just wanted to point out that I really would have pulled out some of the metrics calculation and added tests to the DB layer but I was approaching the 1 hour mark and wanted to make sure I didn't go over, so I abandoned the DB tests and simply implemented the functionality in the Program.cs. 

I was planning to refactor out of some of the poor patterns used in the code too which I ended up not having time to do.

## Assumptions

* I have done some type checking and input validation but I've assumed the user will be largely compliant apart from the Credit Score input as that's an explicit one
* Since it's a console app and there's only one thread anyway, everything is synchronous 

## Upgrades

* The inputs need finer validation
* The calculator for the metrics is optimistic in the values 
* Needs have a DI model
* Need to move the metrics calculation out to a calculator so it can be fully tested in isolation
* There needs to be testing from the outside in where we call the actual console app with specific values and ensure the output is correct
* Needs any tests on the DB layer 
* The application processor isn't really good code, it definitely needs thinking about and abstracting out into their own distinct strategies.

