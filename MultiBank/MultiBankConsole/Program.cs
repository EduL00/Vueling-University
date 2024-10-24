using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Runtime.Serialization.Formatters;

int num_users = 3;
List<string> accounts = new() { "123", "456", "789" };
List<string> pins = new() { "0000", "1111", "2222" };
List<decimal> curr_money = new() { 647, 4671, 87462 };
List<List<decimal>> movements = new(num_users);
int index = 0;
string? account_number;
var success_color = ConsoleColor.Green;
var error_color = ConsoleColor.Red;
var font_color = ConsoleColor.White;

for (int i = 0; i < num_users; ++i)
    movements.Add(new List<decimal>());

while (true)
{
    do
    {
        Console.Clear();
        Console.Write("Enter your account number to login or 'exit' to close the app: ");
        account_number = Console.ReadLine();

        if (account_number == "exit" || account_number == "Exit")
            return;

        if (account_number == null)
        {
            Console.ForegroundColor = error_color;
            Console.WriteLine("Invalid account number.");
            Console.ForegroundColor = font_color;
            Console.WriteLine("Press any key to retry");
            Console.ReadKey();
            continue;
        }

        index = accounts.IndexOf(account_number);

        if (index == -1)
        {
            Console.ForegroundColor = error_color;
            Console.WriteLine("The account does not exists.");
            Console.ForegroundColor = font_color;
            Console.WriteLine("Press any key to retry");
            Console.ReadKey();
            account_number = null;
            continue;
        }

        int     max_attempts = 3;
        int     n_attempt = 1;
        string? pin_number;
        bool    login_sucess = false;

        do
        {
            Console.Write("Enter the pin of the account: ");
            pin_number = Console.ReadLine();

            if (pin_number == null)
            {
                Console.ForegroundColor = error_color;
                Console.WriteLine("Invalid pin number.");
                Console.ForegroundColor = font_color;
                ++n_attempt;
            }
            else
            {
                if (pin_number == pins[index])
                {
                    Console.ForegroundColor = success_color;
                    Console.WriteLine("Login Successfully.");
                    Console.ForegroundColor = font_color;
                    Console.WriteLine("Press any button to enter the main menu");
                    Console.ReadKey();
                    login_sucess = true;
                }
                else
                {
                    Console.ForegroundColor = error_color;
                    Console.WriteLine("Incorrect pin");
                    Console.ForegroundColor = font_color;
                    ++n_attempt;
                }

            }

        } while ((n_attempt <= max_attempts) && (login_sucess == false));

        if (login_sucess == false)
        {
            Console.ForegroundColor = error_color;
            Console.WriteLine("Error in login");
            Console.ForegroundColor = font_color;
            Console.WriteLine("Press any key to retry");
            Console.ReadKey();
            account_number = null;
        }

    } while (account_number == null);


    string? user_option;
    const string exit_option = "7";

    Console.OutputEncoding = System.Text.Encoding.UTF8;

    do
    {
        bool is_decimal;

        Console.Clear();
        Console.WriteLine("====================================");
        Console.WriteLine("1. Money Inconme\n");
        Console.WriteLine("2. Money Outcome\n");
        Console.WriteLine("3. List all movements\n");
        Console.WriteLine("4. List incomes\n");
        Console.WriteLine("5. List outcomes\n");
        Console.WriteLine("6. Show current money\n");
        Console.WriteLine($"{exit_option}. Exit");
        Console.WriteLine("====================================");
        Console.Write("Choose an option:");

        user_option = Console.ReadLine();

        switch (user_option)
        {
            case "1":
                {
                    string? money_income;
                    decimal parsed_income;


                    Console.Write("Enter the money to make the income: ");
                    money_income = Console.ReadLine();

                    is_decimal = decimal.TryParse(money_income, out parsed_income);

                    if ((is_decimal) && (parsed_income > 0))
                    {
                        curr_money[index] += parsed_income;
                        movements[index].Add(parsed_income);
                        Console.ForegroundColor = success_color;
                        Console.WriteLine($"Added {parsed_income}€. Your Current money is: {curr_money[index]}€.");
                        Console.ForegroundColor = font_color;

                    }
                    else
                    {
                        Console.ForegroundColor = error_color;
                        Console.WriteLine("Invalid income value");
                        Console.ForegroundColor = font_color;
                    }

                    break;
                }
            case "2":
                {
                    string? money_outcome;
                    decimal parsed_outcome;

                    Console.Write("Enter the money to make the outcome: ");
                    money_outcome = Console.ReadLine();

                    is_decimal = decimal.TryParse(money_outcome, out parsed_outcome);

                    if ((is_decimal) && (parsed_outcome > 0))
                    {
                        curr_money[index] -= parsed_outcome;
                        movements[index].Add(-(parsed_outcome));
                        Console.ForegroundColor = success_color;
                        Console.WriteLine($"Retired {parsed_outcome}€. Your Current money is: {curr_money[index]}€.");
                        Console.ForegroundColor = font_color;
                    }
                    else
                    {
                        Console.ForegroundColor = error_color;
                        Console.WriteLine("Invalid income value");
                        Console.ForegroundColor = font_color;
                    }
                    break;
                }
            case "3":
                {
                    if (movements[index].Count() == 0) Console.WriteLine("No movements so far.");

                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("=======================");
                        Console.WriteLine("List of all movements");
                        Console.WriteLine("=======================");

                        for (int i = 0; i < movements[index].Count(); ++i)
                        {
                            if (movements[index][i] > 0) Console.WriteLine($"{movements[index][i]}€");
                            else Console.WriteLine($"{movements[index][i]}€");
                        }
                        Console.WriteLine("------------------");
                        Console.WriteLine($"Total: {curr_money[index]}€");
                    }

                    break;
                }
            case "4":
                {
                    decimal total_income = 0;
                    bool has_income = false;


                    for (int i = 0; i < movements[index].Count(); ++i)
                    {
                        if (movements[index][i] > 0)
                        {
                            if (has_income == false)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("================");
                                Console.WriteLine("List of Incomes");
                                Console.WriteLine("================");
                                has_income = true;
                            }

                            total_income += movements[index][i];
                            Console.WriteLine($"{movements[index][i]}€");
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


                    break;
                }
            case "5":
                {
                    bool has_outcomes = false;
                    decimal total_outcome = 0;

                    for (int i = 0; i < movements[index].Count(); ++i)
                    {
                        if (movements[index][i] < 0)
                        {
                            if (has_outcomes == false)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("===================");
                                Console.WriteLine("List of Outcomes");
                                Console.WriteLine("===================");
                                has_outcomes = true;
                            }

                            total_outcome += -(movements[index][i]);
                            Console.WriteLine($"{-(movements[index][i])}€");
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

                    break;
                }
            case "6":
                {
                    Console.WriteLine("Your current money: " + curr_money[index] + "€");
                    break;
                }
            case exit_option:
                {
                    Console.WriteLine("Your current money is: " + curr_money[index] + "€");
                    break;
                }
            default:
                {
                    Console.ForegroundColor = error_color;
                    Console.WriteLine("Invalid option\n");
                    Console.ForegroundColor = font_color;
                    break;
                }
        }

        if (user_option != exit_option)
        {
            string? user_cont;
            Console.WriteLine("Do you want to do any other operation?");
            Console.WriteLine("(Y)es or (N)o");
            user_cont = Console.ReadLine();

            if ((user_cont == "N") || (user_cont == "n"))
            {
                Console.WriteLine("Your current money is: " + curr_money[index] + "€");
                break;
            }
        }

    } while (user_option != exit_option);
}
