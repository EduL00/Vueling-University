using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Classes.Classes;

namespace Classes.Controllers
{
    public class ControllerMainMenu
    {
        private ConsoleColor succ_color = ConsoleColor.Green;
        private ConsoleColor err_color = ConsoleColor.Red;
        private ConsoleColor std_color = ConsoleColor.White;//elc mirar lo de los colores
        public ControllerMainMenu() { }

        public void ShowMainMenu(User user)
        {
            string? user_option;
            const string exit_option = "7";
            do
            {
                user_option = ReadOption();
                ProcessOption(user_option, user);

                if (user_option != exit_option)
                {
                    string? user_cont;
                    Console.WriteLine("Do you want to do any other operation?");
                    Console.WriteLine("(Y)es or (N)o");
                    user_cont = Console.ReadLine();

                    if (user_cont == "N" || user_cont == "n")
                    {
                        Console.WriteLine($"Your current money: {user.Money}€");
                        break;
                    }
                }

            } while (user_option != "7");
        }

        private string ReadOption()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("1. Money Inconme");
            Console.WriteLine("2. Money Outcome");
            Console.WriteLine("3. List all movements");
            Console.WriteLine("4. List incomes");
            Console.WriteLine("5. List outcomes");
            Console.WriteLine("6. Show current money");
            Console.WriteLine("7. Exit");
            Console.WriteLine("====================================");
            Console.Write("Choose an option:");
            return Console.ReadLine();
        }

        private void ProcessOption(string option, User user)
        {
            switch (option)
            {
                case "1":
                    {
                        bool is_decimal;
                        string? money_income;
                        decimal parsed_income;

                        Console.Write("Enter the money to make the income: ");
                        money_income = Console.ReadLine();

                        is_decimal = decimal.TryParse(money_income, out parsed_income);

                        if (is_decimal && parsed_income > 0)
                        {
                            user.MakeIncome(parsed_income);
                            Console.ForegroundColor = succ_color;
                            Console.WriteLine($"Added {parsed_income}€. Your Current money is: {user.Money}€.");
                            Console.ForegroundColor = std_color;
                        }
                        else
                        {
                            Console.ForegroundColor = succ_color;
                            Console.WriteLine("Invalid income value");
                            Console.ForegroundColor = std_color;
                        }

                        break;
                    }
                case "2":
                    {
                        string? money_outcome;
                        decimal parsed_outcome;
                        bool is_decimal;

                        Console.Write("Enter the money to make the outcome: ");
                        money_outcome = Console.ReadLine();

                        is_decimal = decimal.TryParse(money_outcome, out parsed_outcome);

                        if (is_decimal && parsed_outcome > 0)
                        {
                            user.MakeOutcome(parsed_outcome);
                            Console.ForegroundColor = succ_color;
                            Console.WriteLine($"Retired {parsed_outcome}€. Your Current money is: {user.Money}€.");
                            Console.ForegroundColor = std_color;
                        }
                        else
                        {
                            Console.ForegroundColor = err_color;
                            Console.WriteLine("Invalid income value");
                            Console.ForegroundColor = std_color;
                        }

                        break;
                    }
                case "3":
                    {
                        int i = 0;
                        int user_movements = user.GetNMovements();
                        if (user_movements == 0) Console.WriteLine("No movements so far.");

                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("=======================");
                            Console.WriteLine("List of all movements");
                            Console.WriteLine("=======================");

                            while (i < user_movements)
                            {
                                Movement mov = user.GetMovement(i);

                                if (mov.Type == MovementType.Income) Console.WriteLine($"{mov.Value}€");
                                else Console.WriteLine($"-{mov.Value}€");

                                ++i;
                            }


                            Console.WriteLine("------------------");
                            Console.WriteLine($"Total: {user.Money}€");
                        }
                        break;
                    }
                case "4":
                    {
                        int user_movements;
                        int i = 0;
                        decimal total_income = 0;
                        bool has_income = false;

                        user_movements = user.GetNMovements();

                        while (i < user_movements)
                        {
                            Movement mov = user.GetMovement(i);

                            if (mov.Type == MovementType.Income)
                            {
                                if (has_income == false)
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("================");
                                    Console.WriteLine("List of Incomes");
                                    Console.WriteLine("================");
                                    has_income = true;
                                }

                                total_income += mov.Value;
                                Console.WriteLine($"{mov.Value}€");
                            }

                            ++i;
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

                        break;
                    }
                case "5":
                    {
                        bool has_outcomes = false;
                        int user_movements;
                        int i = 0;
                        decimal total_outcome = 0;

                        user_movements = user.GetNMovements();

                        while (i < user_movements)
                        {
                            Movement mov = user.GetMovement(i);

                            if (mov.Type == MovementType.Outcome)
                            {
                                if (has_outcomes == false)
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("================");
                                    Console.WriteLine("List of Outcomes");
                                    Console.WriteLine("================");
                                    has_outcomes = true;
                                }

                                total_outcome += -mov.Value;
                                Console.WriteLine($"{mov.Value}€");
                            }

                            ++i;
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
                        break;
                    }
                case "6":
                    {
                        Console.WriteLine($"Your current money: {user.Money}€");
                        break;
                    }
                case "7":
                    {
                        Console.WriteLine($"Your current money: {user.Money}€");
                        return;
                    }
                default:
                    {
                        Console.ForegroundColor = err_color;
                        Console.WriteLine("Invalid option\n");
                        Console.ForegroundColor = std_color;
                        break;
                    }
            }
        }
    }
}
