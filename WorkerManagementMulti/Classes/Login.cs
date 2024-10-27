using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Login
    {
        Manager Manager;
        public Login()
        {
            Manager = new Manager();
        }

        public void ShowMenu(string id)
        {
            string? user_option;
            const string admin_exit_option = "13";
            const string manager_exit_option = "6";
            const string worker_exit_option = "4";
            ConsoleColor succ_color = ConsoleColor.Green;
            ConsoleColor err_color = ConsoleColor.Red;
            ConsoleColor std_color = ConsoleColor.White;//elc mirar lo de color
            ITWorker worker = null;
            MenuRole menu_role;

            if (id != "0")
            {
                if (Manager.LookForWorker(id, out worker) == false)
                {
                    Console.ForegroundColor = err_color;
                    Console.WriteLine("Worker not found");
                    Console.ForegroundColor = std_color;
                    return;
                }
            }

            if (id == "0")
            {
                menu_role = MenuRole.Admin;
            }
            else
            {
                if (worker.IsManager)
                    menu_role = MenuRole.Manager;
                else
                    menu_role = MenuRole.Worker;
            }

            do
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                if (menu_role == MenuRole.Admin)
                {
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
                    Console.WriteLine($"{admin_exit_option}. Exit");
                }
                else if (menu_role == MenuRole.Manager)
                {
                    Console.WriteLine("1. List team members by team name");
                    Console.WriteLine("2. List unassigned tasks");
                    Console.WriteLine("3. List task assignments by team name");
                    Console.WriteLine("4. Assign IT worker to a team as technician");
                    Console.WriteLine("5. Assign task to IT worker");
                    Console.WriteLine($"{manager_exit_option}. Exit");

                }
                else
                {
                    Console.WriteLine("1  List unassigned tasks");
                    Console.WriteLine("2. List task assignments by team name");
                    Console.WriteLine("3. Assign task to IT worker");
                    Console.WriteLine($"{worker_exit_option}. Exit");

                }

                Console.WriteLine("==============================================");
                Console.Write("Choose an option: ");
                user_option = Console.ReadLine();

                switch (user_option)
                {
                    case "1":
                        {
                            if (menu_role == MenuRole.Admin)
                                Manager.RegisterITWorker();
                            else if (menu_role == MenuRole.Manager)
                                Manager.ListTeamMembers(menu_role, worker);
                            else
                                Manager.ListUnassignedTasks();
                            break;
                        }
                    case "2":
                        {
                            if (menu_role == MenuRole.Admin)
                                Manager.ListITWorkers();
                            else if (menu_role == MenuRole.Manager)
                                Manager.ListUnassignedTasks();
                            else
                                Manager.ListWorkerTeamAssigments(worker);

                            break;
                        }
                    case "3":
                        {
                            if (menu_role == MenuRole.Admin)
                                Manager.RegisterTeam();
                            else if (menu_role == MenuRole.Manager)
                                Manager.ListTaskAssignementsTeam(menu_role, worker);
                            else
                                Manager.SetYourselfToTask(worker);

                            break;
                        }
                    case "4":
                        {
                            if (menu_role == MenuRole.Admin)
                            {
                                Manager.RegisterTask();
                            }
                            else if (menu_role == MenuRole.Manager)
                            {
                                Manager.SetTeamTechnician();
                            }
                            else
                            {
                                Console.ForegroundColor = err_color;
                                Console.WriteLine("Invalid Option");
                                Console.ForegroundColor = std_color;
                            }

                            break;
                        }
                    case "5":
                        {
                            if (menu_role == MenuRole.Admin)
                            {
                                Manager.ListTeams();
                            }
                            else if (menu_role == MenuRole.Manager)
                            {
                                Manager.SetWorkerToTask();
                            }
                            else
                            {
                                Console.ForegroundColor = err_color;
                                Console.WriteLine("Invalid Option");
                                Console.ForegroundColor = std_color;
                            }
                            break;
                        }
                    case "6":
                        {
                            if (menu_role == MenuRole.Admin)
                            {
                                Manager.ListTeamMembers(menu_role, worker);
                            }
                            else
                            {
                                Console.ForegroundColor = err_color;
                                Console.WriteLine("Invalid Option");
                                Console.ForegroundColor = std_color;
                            }
                            break;
                        }
                    case "7":
                        {
                            if (menu_role == MenuRole.Admin)
                            {
                                Manager.ListUnassignedTasks();
                            }
                            else
                            {
                                Console.ForegroundColor = err_color;
                                Console.WriteLine("Invalid Option");
                                Console.ForegroundColor = std_color;
                            }
                            break;
                        }
                    case "8":
                        {
                            if (menu_role == MenuRole.Admin)
                            {
                                Manager.ListTaskAssignementsTeam(menu_role, worker);
                            }
                            else
                            {
                                Console.ForegroundColor = err_color;
                                Console.WriteLine("Invalid Option");
                                Console.ForegroundColor = std_color;
                            }
                            break;
                        }
                    case "9":
                        {
                            if (menu_role == MenuRole.Admin)
                            {
                                Manager.SetTeamManager();
                            }
                            else
                            {
                                Console.ForegroundColor = err_color;
                                Console.WriteLine("Invalid Option");
                                Console.ForegroundColor = std_color;
                            }
                            break;
                        }
                    case "10":
                        {
                            if (menu_role == MenuRole.Admin)
                            {
                                Manager.SetTeamTechnician();
                            }
                            else
                            {
                                Console.ForegroundColor = err_color;
                                Console.WriteLine("Invalid Option");
                                Console.ForegroundColor = std_color;
                            }
                            break;
                        }
                    case "11":
                        {
                            if (menu_role == MenuRole.Admin)
                            {
                                Manager.SetWorkerToTask();
                            }
                            else
                            {
                                Console.ForegroundColor = err_color;
                                Console.WriteLine("Invalid Option");
                                Console.ForegroundColor = std_color;
                            }
                            break;
                        }
                    case "12":
                        {
                            if (menu_role == MenuRole.Admin)
                            {
                                Manager.UnregisterWorker();
                            }
                            else
                            {
                                Console.ForegroundColor = err_color;
                                Console.WriteLine("Invalid Option");
                                Console.ForegroundColor = std_color;
                            }
                            break;
                        }
                    case admin_exit_option:
                        {
                            if (menu_role != MenuRole.Admin)
                            {
                                Console.ForegroundColor = err_color;
                                Console.WriteLine("Invalid Option");
                                Console.ForegroundColor = std_color;
                            }
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

                if ((user_option != admin_exit_option && menu_role == MenuRole.Admin) || ((user_option != manager_exit_option) && (menu_role == MenuRole.Manager) || ((user_option != worker_exit_option) && (menu_role == MenuRole.Worker))))
                {
                    string? user_cont;
                    Console.WriteLine("Do you want to do any other operation?");
                    Console.WriteLine("(Y)es or (N)o");
                    user_cont = Console.ReadLine();

                    if ((user_cont == "N") || (user_cont == "n")) return;
                }

            } while (((user_option != admin_exit_option) && (menu_role == MenuRole.Admin)) || ((user_option != manager_exit_option) && (menu_role == MenuRole.Manager)) || ((user_option != worker_exit_option) && (menu_role == MenuRole.Worker)));
        }
    }
}
