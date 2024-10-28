using System;
using System.Reflection;


namespace Classes.Classes
{
    public class User
    {
        public string Id { get; set; }
        public string Pin { get; set; }

        private ConsoleColor succ_color = ConsoleColor.Green;
        private ConsoleColor err_color = ConsoleColor.Red;
        private ConsoleColor std_color = ConsoleColor.White;

        List<Movement> Movements { get; set; }
        public decimal Money { get; set; }

        public User()
        {
            Id = "0";
            Pin = "0";
            Movements = new List<Movement>();
            Money = 0;
        }

        public User(string id, string pin, int money)
        {
            Id = id;
            Pin = pin;
            Movements = new List<Movement>();
            Money = money;
        }

        public void MakeIncome(decimal income)
        {
            Movement mov = new Movement(MovementType.Income, income);

            Money += income;
            Movements.Add(mov);
        }

        public void MakeOutcome(decimal outcome)
        {
            Movement mov = new Movement(MovementType.Outcome, outcome);

            Money -= outcome;
            Movements.Add(mov);
        }

        public int GetNMovements()
        {
            return Movements.Count;
        }

        public Movement GetMovement (int i)
        {
            return Movements[i];
        }
    }

}
