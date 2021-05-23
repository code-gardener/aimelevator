# aimelevator
Elevator simulator

# Description
This Api simulates an Elevator.

Clone the site and hit F5.

This site uses port 53375.  This against the directions.  My local machine is set up to use 8080 for work purposes and I didn't want undo all of that.

Exercise the API as follows.

The Elevator is initially set with no floors selected.  In this state the current floor is -1.
To request the elevator to pick up a passenger. use http://localhost:6081/api/elevator/requestElevator?floor=the passengers floor
This will return the current floor of the elevator.
To send the elevator to stop at a floor, use http://localhost:6081/api/elevator/sendElevator?destinationFloor=the destination floor
This will return the current floor of the elevator.
To get the next floor the elevator will stop at use http://localhost:6081/api/elevator/getNextFloor
This will return the the next floor the elevator will stop at.
To get all of the floors selected. use http://localhost:6081/api/elevator/getAllFloors
This will return a list of floors.
You can move the elevator to the next floor.  use http://localhost:6081/api/elevator/moveElevator?direction= the elevator direction.  0 is for down.  1 is for up.