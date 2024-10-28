using Classes;

var err_color = ConsoleColor.Red;
var std_color = ConsoleColor.White;
var succ_color = ConsoleColor.Green;
Login login = new Login();
MainMenu main_menu = new MainMenu();

Console.OutputEncoding = System.Text.Encoding.UTF8;

while (true)
{
    string? account_number;
    User user = new();

    if (login.WantToLogin(out account_number) == false) return;

    if (login.TryToLogin(account_number, out user))
    {
        Console.ForegroundColor = succ_color;
        Console.WriteLine("Login Successfully");
        Console.ForegroundColor = std_color;
        Console.WriteLine("Press any button to enter the main menu");
        Console.ReadKey();

        main_menu.ShowMainMenu(user);
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