
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Xml.Linq;
using Classes;

Login login = new();

while (true)
{
    string? id;
    Console.Write("Enter a worker ID (0 to enter in admin mode) or exit to exit the app: ");
    id = Console.ReadLine();
    if (id == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid Id");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Press any button to retry");
        Console.ReadKey();
        continue;
    }
    else if (id == "exit")
    {
        return;
    }

    login.ShowMenu(id);
}
