using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public List<int> Pickup { get; set; }
        public List<int> DropOff { get; set; }
        public int Direction { get; set; }
    }
}
