using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using ElevatorAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElevatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorController : ControllerBase
    {
        public Car car = new Car() { Id = 1, DropOff = new List<int>(), Pickup = new List<int>() };

        public ElevatorController()
        {
        }

        // GET: api/<ElevatorController>
        [HttpGet("allRequestedFloors")]
        public IEnumerable<int> AllRequestedFloors()
        {
            List<int> allFloors = new List<int>();
            allFloors.AddRange(car.Pickup);
            allFloors.AddRange(car.DropOff);
            allFloors.Sort();

            return allFloors;
        }

        // GET api/<ElevatorController>/5
        [HttpGet("getNextFloor")]
        public int GetNextFloor()
        {
            int nextFloor = -1;
            int nextPickupFloor = -1;
            int nextDropOffFloor = -1;

            if (car.DropOff.Count == 0 && car.Pickup.Count == 0)
            {
                return nextFloor;  // nothing to service.
            }

            if (car.Direction == 0)
            {
                // Get the Pickup Floor <= Current Floor
                if (car.Pickup.Count > 0)
                {
                    nextPickupFloor = car.Pickup.Select(x => x).Where(x => x < car.CurrentFloor).FirstOrDefault();
                }
                // Get the DropOff Floor >= Current Floor
                if (car.DropOff.Count > 0)
                {
                    nextDropOffFloor = car.Pickup.Select(x => x).Where(x => x > car.CurrentFloor).FirstOrDefault();
                }
            }
            else
            {
                // Get the Pickup Floor ?= Current Floor
                if (car.Pickup.Count > 0)
                {
                    nextPickupFloor = car.Pickup.Select(x => x).Where(x => x > car.CurrentFloor).FirstOrDefault();
                }
                // Get the DropOff Floor <= Current Floor
                if (car.DropOff.Count > 0)
                {
                    nextDropOffFloor = car.Pickup.Select(x => x).Where(x => x < car.CurrentFloor).FirstOrDefault();
                }
            }

            if (nextDropOffFloor == -1)
            {
                return nextPickupFloor;
            }
            else if (nextPickupFloor == -1)
            {
                return nextDropOffFloor;
            }
            else
            {
                return Math.Min(nextPickupFloor, nextDropOffFloor);
            }
        }

        // GET api/<ElevatorController>/requestElevator?floor=5
        [HttpGet("requestElevator")]
        public int RequestElevator([FromQuery] int floor)
        {
            car.Pickup.Add(floor);
            return car.CurrentFloor;
        }

        // GET api/<ElevatorController>/sendElevator?destinationFloor=5
        [HttpGet("sendElevator")]
        public int SendElevator([FromQuery] int destinationFloor)
        {
            car.DropOff.Add(destinationFloor);
            return car.CurrentFloor;
        }

        // GET api/<ElevatorController>/?moveElevator?direction=15
        [HttpGet("moveElevator")]
        public int MoveElevator([FromQuery] int direction)
        {
            car.Direction = Convert.ToInt32(direction);
            car.CurrentFloor = this.GetNextFloor();
            car.DropOff.Remove(car.CurrentFloor);
            car.Pickup.Remove(car.CurrentFloor);

            return car.CurrentFloor;
        }
    }
}
