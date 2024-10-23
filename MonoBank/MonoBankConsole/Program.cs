decimal       user_income = 0;
List<decimal> user_movements = new();
string        user_option;
const string  exit_option = "7";

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
                string money_income;
                decimal parsed_income;


                Console.Write("Enter the money to make the income: ");
                money_income = Console.ReadLine();

                is_decimal = decimal.TryParse(money_income, out parsed_income);

                if ((is_decimal) && (parsed_income > 0))
                {
                    user_income += parsed_income;
                    user_movements.Add(parsed_income);

                }
                else
                {
                    Console.WriteLine("Invalid income value");
                }

                break;
            }
        case "2":
            {
                string  money_outcome;
                decimal parsed_outcome;

                Console.Write("Enter the money to make the outcome: ");
                money_outcome = Console.ReadLine();

                is_decimal = decimal.TryParse(money_outcome, out parsed_outcome);

                if ((is_decimal) && (parsed_outcome > 0))
                {
                    user_income -= parsed_outcome;
                    user_movements.Add(-(parsed_outcome));

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
                user_movements.ForEach(x => Console.WriteLine(x));
                break;
            }
        case "4":
            {
                Console.WriteLine("List of Incomes\n");
                user_movements.ForEach(x => { if (x > 0) Console.WriteLine(x); });
                break;
            }
        case "5":
            {
                Console.WriteLine("List of Outcomes\n");
                user_movements.ForEach(x => { if (x < 0) Console.WriteLine(x); });
                break;
            }
        case "6":
            {
                Console.WriteLine("Your current money: " + user_income+ " €");
                break;
            }
        case exit_option:
            {
                Console.WriteLine("Your current money is: " + user_income+ " €");
                break;
            }
        default:
            {
                Console.WriteLine("Invalid option\n");
                break;
            }
    }

    if (user_option != exit_option)
    {
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }


} while (user_option != exit_option);