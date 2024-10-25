using System;
using System.Reflection;
using Classes;


namespace Classes
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

        public User (string id, string pin, int money)
        {
            Id = id;
            Pin = pin;
            Movements = new List<Movement>();
            Money = money;
        }

        public void MakeIncome()
        {
            bool    is_decimal;
            string? money_income;
            decimal parsed_income;


            Console.Write("Enter the money to make the income: ");
            money_income = Console.ReadLine();

            is_decimal = decimal.TryParse(money_income, out parsed_income);

            if ((is_decimal) && (parsed_income > 0))
            {
                Movement mov = new Movement(MovementType.Income,parsed_income);

                Money += parsed_income;
                Movements.Add(mov);
                Console.ForegroundColor = succ_color;
                Console.WriteLine($"Added {parsed_income}€. Your Current money is: {Money}€.");
                Console.ForegroundColor = std_color;

            }
            else
            {
                Console.ForegroundColor = succ_color;
                Console.WriteLine("Invalid income value");
                Console.ForegroundColor = std_color;
            }

        }
        
        public void MakeOutcome ()
        {
            string? money_outcome;
            decimal parsed_outcome;
            bool    is_decimal;

            Console.Write("Enter the money to make the outcome: ");
            money_outcome = Console.ReadLine();

            is_decimal = decimal.TryParse(money_outcome, out parsed_outcome);

            if ((is_decimal) && (parsed_outcome > 0))
            {
                Movement mov = new Movement(MovementType.Outcome, parsed_outcome);

                Money -= parsed_outcome;
                Movements.Add(mov);
                Console.ForegroundColor = succ_color;
                Console.WriteLine($"Retired {parsed_outcome}€. Your Current money is: {Money}€.");
                Console.ForegroundColor = std_color;
            }
            else
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Invalid income value");
                Console.ForegroundColor = std_color;
            }

        }

        public void ListIncomes ()
        {
            decimal total_income = 0;
            bool has_income = false;

            for (int i = 0; i < Movements.Count(); ++i)
            {
                if (Movements[i].Type == MovementType.Income)
                {
                    if (has_income == false)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("================");
                        Console.WriteLine("List of Incomes");
                        Console.WriteLine("================");
                        has_income = true;
                    }

                    total_income += Movements[i].Value;
                    Console.WriteLine($"{Movements[i].Value}€");
                }
            }

            if (has_income)
            {
                Console.WriteLine("------------------");
                Console.WriteLine($"Total: {total_income}€");
            }
            else
            {
                Console.WriteLine("No incomes so far.");
            }
        }

        public void ListOutcomes ()
        {
            bool has_outcomes = false;
            decimal total_outcome = 0;

            for (int i = 0; i < Movements.Count(); ++i)
            {
                if (Movements[i].Type == MovementType.Outcome)
                {
                    if (has_outcomes == false)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("===================");
                        Console.WriteLine("List of Outcomes");
                        Console.WriteLine("===================");
                        has_outcomes = true;
                    }

                    total_outcome += -(Movements[i].Value);
                    Console.WriteLine($"{Movements[i].Value}€");
                }
            }

            if (has_outcomes)
            {
                Console.WriteLine("------------------");
                Console.WriteLine($"Total: {total_outcome}€");
            }
            else
            {
                Console.WriteLine("No outcomes so far.");
            }
        }

        public void ListMovements()
        {
            if (Movements.Count() == 0) Console.WriteLine("No movements so far.");

            else
            {
                Console.WriteLine("");
                Console.WriteLine("=======================");
                Console.WriteLine("List of all movements");
                Console.WriteLine("=======================");

                for (int i = 0; i < Movements.Count(); ++i)
                {
                    if (Movements[i].Type == MovementType.Income) Console.WriteLine($"{Movements[i].Value}€");
                    else Console.WriteLine($"-{Movements[i].Value}€");
                }
                Console.WriteLine("------------------");
                Console.WriteLine($"Total: {Money}€");
            }

        }

        public void ShowCurrMoney()
        {
            Console.WriteLine($"Your current money: {Money}€");
        }


    }

}
