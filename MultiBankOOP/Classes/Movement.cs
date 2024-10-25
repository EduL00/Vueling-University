using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Movement
    {
        public enum MovementType
        {
            Income = 1,
            Outcome = 2,
            NotValid = 3
        }
        public MovementType Type { get; set; }
        public int Value { get; set; }
        public Movement() { }

        //elc Quizas hacer un diccionario para parsear
        public string MovementTypeToString(MovementType type_to_parse)
        {
            if (type_to_parse == MovementType.Income) return "Income";
            else if (type_to_parse == MovementType.Outcome) return "Outcome";

            return "No valid movement type";
        }

        public MovementType StringToMovementType(string string_to_parse)
        {
            if (string_to_parse == "1") return MovementType.Income;
            else if (string_to_parse == "2") return MovementType.Outcome;

            return MovementType.NotValid;
        }
    }
}
