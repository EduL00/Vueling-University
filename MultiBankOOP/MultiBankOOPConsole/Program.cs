using Classes;

var err_color = ConsoleColor.Red;
var std_color = ConsoleColor.White;
var succ_color = ConsoleColor.Green;

while (true)
{
    //elc Quizas hacer un gestor de menus
    string? account_number;
    Login login = new Login();

    Console.Clear();
    Console.Write("Enter your account number to login or 'exit' to close the app: ");
    account_number = Console.ReadLine();

    if (account_number == "exit" || account_number == "Exit")
        return;

    if (account_number == null)
    {
        Console.ForegroundColor = err_color;
        Console.WriteLine("Invalid account number.");
        Console.ForegroundColor = std_color;
        Console.WriteLine("Press any key to retry");
        Console.ReadKey();
        continue;
    }
    else
    {
        if (login.TryToLogin(account_number))
        {
            Console.ForegroundColor = succ_color;
            Console.WriteLine("Login Successfully");
            Console.ForegroundColor = std_color;
            Console.WriteLine("Press any button to enter the main menu");
            Console.ReadKey();

        }
        else
        {
            Console.ForegroundColor = err_color;
            Console.WriteLine("Error in login");
            Console.ForegroundColor = std_color;
            Console.WriteLine("Press any key to retry");
            Console.ReadKey();
            continue;
        }
    }
}
