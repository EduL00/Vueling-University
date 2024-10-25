
using System.Threading.Tasks;
using System.Xml.Linq;

string? user_option;
const string exit_option = "12";
do
{
    Console.Clear();
    Console.WriteLine("==============================================");
    Console.WriteLine("1.  Register new IT worker");
    Console.WriteLine("2.  Register new team");
    Console.WriteLine("3.  Register new task (unassigned to anyone)");
    Console.WriteLine("4.  List all team names");
    Console.WriteLine("5.  List team members by team name");
    Console.WriteLine("6.  List unassigned tasks");
    Console.WriteLine("7.  List task assignments by team name");
    Console.WriteLine("8.  Assign IT worker to a team as manager");
    Console.WriteLine("9.  Assign IT worker to a team as technician");
    Console.WriteLine("10. Assign task to IT worker");
    Console.WriteLine("11. Unregister IT worker");
    Console.WriteLine($"{exit_option}. Exit");
    Console.WriteLine("==============================================");
    Console.Write("Choose an option: ");
    user_option = Console.ReadLine();

} while (user_option != exit_option);