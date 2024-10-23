using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Runtime.Serialization.Formatters;

int                 num_users = 3;
List<string>        accounts = new() { "123", "456", "789"};
List<string>        pins = new() { "0000", "1111", "2222" };
List<decimal>       curr_money = new() { 647, 4671, 87462};
List<List<decimal>> movements = new(num_users);
int                 index = 0;
string              ? account_number;

for (int i = 0; i < num_users; ++i)
    movements.Add(new List<decimal>());

do
{
    Console.Write("Enter your account number: ");
    account_number = Console.ReadLine();

    if (account_number == null)
        Console.WriteLine("Invalid account number.");
    else
    {
       index = accounts.IndexOf(account_number);

        if (index == -1)
        {
            Console.WriteLine("The account does not exists.");
            account_number = null;
        }
        else
        {
            int max_attempts = 3;
            int n_attempt = 1;
            string? pin_number;
            bool login_sucess = false;

            do
            {
                Console.Write("Enter the pin of the account: ");
                pin_number = Console.ReadLine();

                if (pin_number == null)
                {
                    Console.WriteLine("Invalid pin number.");
                    ++n_attempt;
                }
                else
                {
                    if (pin_number == pins[index])
                    {
                        Console.WriteLine("Login Successfully.");
                        login_sucess = true;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect pin");
                        ++n_attempt;
                    }

                }

            } while ((n_attempt <= max_attempts) && (login_sucess == false));

            if (login_sucess == false)
            {
                Console.WriteLine("Error in login");
                account_number = null;
            }
            

        }
    }

} while (account_number == null);


string ? user_option;
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
                string ? money_income;
                decimal parsed_income;


                Console.Write("Enter the money to make the income: ");
                money_income = Console.ReadLine();

                is_decimal = decimal.TryParse(money_income, out parsed_income);

                if ((is_decimal) && (parsed_income > 0))
                {
                    curr_money[index]+= parsed_income;
                    movements[index].Add(parsed_income);

                }
                else
                {
                    Console.WriteLine("Invalid income value");
                }

                break;
            }
        case "2":
            {
                string ? money_outcome;
                decimal parsed_outcome;

                Console.Write("Enter the money to make the outcome: ");
                money_outcome = Console.ReadLine();

                is_decimal = decimal.TryParse(money_outcome, out parsed_outcome);

                if ((is_decimal) && (parsed_outcome > 0))
                {
                    curr_money[index] -= parsed_outcome;
                    movements[index].Add(-(parsed_outcome));

                }
                else
                {
                    Console.WriteLine("Invalid income value");
                }
                break;
            }
        case "3":
            {
                Console.WriteLine("List of all movements\n");
                movements[index].ForEach(x => Console.WriteLine(x));
                break;
            }
        case "4":
            {
                Console.WriteLine("List of Incomes\n");
                movements[index].ForEach(x => { if (x > 0) Console.WriteLine(x); });
                break;
            }
        case "5":
            {
                Console.WriteLine("List of Outcomes\n");
                movements[index].ForEach(x => { if (x < 0) Console.WriteLine(x); });
                break;
            }
        case "6":
            {
                Console.WriteLine("Your current money: " + curr_money[index] + " €");
                break;
            }
        case exit_option:
            {
                Console.WriteLine("Your current money is: " + curr_money[index] + " €");
                break;
            }
        default:
            {
                Console.WriteLine("Invalid option\n");
                break;
            }
    }


} while (user_option != exit_option);
