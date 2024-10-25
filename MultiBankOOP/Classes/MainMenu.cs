using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class MainMenu
    {
        private ConsoleColor succ_color = ConsoleColor.Green;
        private ConsoleColor err_color = ConsoleColor.Red;
        private ConsoleColor std_color = ConsoleColor.White;//elc mirar lo de los colores
        public MainMenu() { }

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

                    if ((user_cont == "N") || (user_cont == "n"))
                    {
                        user.ShowCurrMoney();
                        break;
                    }
                }

            } while (user_option != "7");
        }

        private string ReadOption()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("1. Money Inconme\n");
            Console.WriteLine("2. Money Outcome\n");
            Console.WriteLine("3. List all movements\n");
            Console.WriteLine("4. List incomes\n");
            Console.WriteLine("5. List outcomes\n");
            Console.WriteLine("6. Show current money\n");
            Console.WriteLine($"7. Exit");
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
                        user.MakeIncome();
                        break;
                    }
                case "2":
                    {
                        user.MakeOutcome();
                        break;
                    }
                case "3":
                    {
                        user.ListMovements();
                        break;
                    }
                case "4":
                    {
                        user.ListIncomes();
                        break;
                    }
                case "5":
                    {
                        user.ListOutcomes();
                        break;
                    }
                case "6":
                    {
                        user.ShowCurrMoney();
                        break;
                    }
                case "7":
                    {
                        user.ShowCurrMoney();
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

