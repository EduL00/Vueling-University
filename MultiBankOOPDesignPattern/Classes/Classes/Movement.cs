using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Classes
{
    public enum MovementType
    {
        Income = 1,
        Outcome = 2,
        NotValid = 3
    }
    public class Movement
    {

        public MovementType Type { get; set; }
        public decimal Value { get; set; }
        public Movement() { }
        public Movement(MovementType type, decimal value)
        {
            Type = type;
            Value = value;
        }
    }
}
