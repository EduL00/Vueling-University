using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Login
    {
        List<User> users;
        private ConsoleColor succ_color = ConsoleColor.Green;
        private ConsoleColor err_color = ConsoleColor.Red;
        private ConsoleColor std_color = ConsoleColor.White;//elc mirar lo de los colores
        public Login()
        {
            users = new List<User>();

            //Esto es para generar usuarios al inicializar la clase Login.
            for (int i = 0; i < 3; i++)
            {
                User user = new User($"{i}", $"{i}{i}{i}{i}", i * 10);
                users.Add(user);
            }
        }

        public bool WantToLogin(out string account_number)
        {
            Console.Clear();
            Console.Write("Enter your account number to login or 'exit' to close the app: ");
            account_number = Console.ReadLine();

            if (account_number == "exit" || account_number == "Exit")
                return false;

            return true;
        }
        public bool TryToLogin(string user_id, out User user)
        {
            int user_index;
            user = null;

            if (user_id == null)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Invalid account number.");
                Console.ForegroundColor = std_color;
                return false;
            }

            if (FindUser(user_id, out user_index) == false) return false;

            if (LookForPin(user_index) == false) return false;

            user = users[user_index];
            return true;
        }

        public bool FindUser(string user_id, out int user_index)
        {
            user_index = 0;

            for (int i = 0; i < users.Count; ++i)
            {
                user_index = i;
                if (users[i].Id == user_id) return true;
            }

            return false;
        }

        public bool LookForPin(int user_index)
        {
            var err_color = ConsoleColor.Red;
            var std_color = ConsoleColor.White;
            const int max_attempts = 3;

            for (int attempt = 1; attempt <= max_attempts; ++attempt)
            {
                string? pin;
                Console.Write("Enter the pin of the account: ");
                pin = Console.ReadLine();

                if (pin == null)
                {
                    Console.ForegroundColor = err_color;
                    Console.WriteLine("Invalid pin number.");
                    Console.ForegroundColor = std_color;
                }
                else
                {
                    if (users[user_index].Pin == pin)
                    {
                        return true;
                    }
                    else
                    {
                        Console.ForegroundColor = err_color;
                        Console.WriteLine("Incorrect pin");
                        Console.ForegroundColor = std_color;
                    }

                }


            }


            return false;
        }
    }
}

