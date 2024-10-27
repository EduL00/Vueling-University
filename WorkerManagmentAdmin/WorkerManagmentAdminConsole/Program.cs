
using System.Threading.Tasks;
using System.Xml.Linq;
using Classes;

string?      user_option;
const string exit_option = "13";
ConsoleColor succ_color = ConsoleColor.Green;
ConsoleColor err_color = ConsoleColor.Red;
ConsoleColor std_color = ConsoleColor.White;//elc mirar lo de color
Manager manager = new();

do
{
    Console.Clear();
    Console.WriteLine("==============================================");
    Console.WriteLine("1.  Register new IT worker");
    Console.WriteLine("2.  List IT workers");
    Console.WriteLine("3.  Register new team");
    Console.WriteLine("4.  Register new task (unassigned to anyone)");
    Console.WriteLine("5.  List all team names");
    Console.WriteLine("6.  List team members by team name");
    Console.WriteLine("7.  List unassigned tasks");
    Console.WriteLine("8.  List task assignments by team name");
    Console.WriteLine("9.  Assign IT worker to a team as manager");
    Console.WriteLine("10. Assign IT worker to a team as technician");
    Console.WriteLine("11. Assign task to IT worker");
    Console.WriteLine("12. Unregister IT worker");
    Console.WriteLine($"{exit_option}. Exit");
    Console.WriteLine("==============================================");
    Console.Write("Choose an option: ");
    user_option = Console.ReadLine();

    switch (user_option)
    {
        case "1":
        {
            manager.RegisterITWorker();
            break;
        }
        case "2":
        {
            manager.ListITWorkers();
            break;
        }
        case "3":
        {
            manager.RegisterTeam();
            break;
        }
        case "4":
            {
                manager.RegisterTask();
                break;
            }
        case "5":
            {
                manager.ListTeams();
                break;
            }
        case "6":
            {
                manager.ListTeamMembers();
                break;
            }
        case "7":
            {
                manager.ListUnassignedTasks();
                break;
            }
        case "8":
            {
                manager.ListTaskAssignementsTeam();
                break;
            }
        case "9":
            {
                manager.SetTeamManager();
                break;
            }
        case "10":
            {
                manager.SetTeamTechnician();
                break;
            }
        case "11":
            {
                manager.SetWorkerToTask();
                break;
            }
        case "12":
            {
                manager.UnregisterWorker();
                break;
            }
        case exit_option:
            {
                break;
            }
        default:
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Invalid option\n");
                Console.ForegroundColor = std_color;
                break;
            }
    }

    if (user_option != exit_option)
    {
        string? user_cont;
        Console.WriteLine("Do you want to do any other operation?");
        Console.WriteLine("(Y)es or (N)o");
        user_cont = Console.ReadLine();

        if ((user_cont == "N") || (user_cont == "n")) return;
    }

} while (user_option != exit_option);