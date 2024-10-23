bool          user_continue;
decimal       user_income = 0;
List<decimal> user_movements = new();

user_continue = true;

while (user_continue)
{
    string        user_option;

    Console.Clear ();
    Console.WriteLine("Choose an option\n");
    Console.WriteLine("1. Money Inconme\n");
    Console.WriteLine("2. Money Outcome\n");
    Console.WriteLine("3. List all movements\n");
    Console.WriteLine("4. List incomes\n");
    Console.WriteLine("5. List outcomes\n");
    Console.WriteLine("6. Show current money\n");
    Console.WriteLine("7. Exit");

    user_option = Console.ReadLine();

    switch (user_option)
    {
        case "1":
            {
                string money_income;

                Console.WriteLine("Introduce the money to make the income.\n");
                money_income = Console.ReadLine();
                user_income += System.Convert.ToDecimal(money_income);
                user_movements.Add(System.Convert.ToDecimal(money_income));
                break;
            }
        case "2":
            {
                string money_outcome;

                Console.WriteLine("Introduce the money to make the outcome.\n");
                money_outcome = Console.ReadLine();
                user_income -= System.Convert.ToDecimal(money_outcome);
                user_movements.Add(-(System.Convert.ToDecimal(money_outcome)));
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
                Console.WriteLine("Current money: "+user_income);
                break;
            }
        case "7":
            {
                user_continue = false;
                break;
            }
        default:
            {
                Console.WriteLine("Invalid option\n");
                break;
            }
    }

    Console.WriteLine("Do you want to make another option?\n");
    Console.WriteLine("(Y)es or (N)o");
    user_option = Console.ReadLine();

    if ((user_option == "Y") || (user_option == "y"))
        user_continue = true;
    else if ((user_option == "N") || (user_option == "n"))
        user_continue = false;
    else
        Console.WriteLine("Invalid Input\n");

}

Console.WriteLine("Your current money is: "+user_income);