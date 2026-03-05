# Step 1

## Testing ##
1- Create controller to control endpoints <br>
2- Fix the program.cs <br>
3- Create the db EmployeeManagement with one table: <b>Employee</b> <br>
4- Add new controller for EmployeeManagement System<BR>
5- Added connection string to appsetings.json (connected to db)

## Architecture of the Program ##
Program.cs ---> Controller ---> Service Layer(show 5 instead of 10 selects example) ---> Repo Layer (db communicator)
