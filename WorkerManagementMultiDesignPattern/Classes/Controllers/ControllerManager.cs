using Classes.Classes;
using Classes.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Controllers
{
    public class ControllerManager : MenuInterface
    {
        List<int> IdManagers;
        private const string exit_option = "6";
        ControllerAdmin Admin;
        public ControllerManager(ControllerAdmin admin)
        {
            IdManagers = new List<int>();
            Admin = admin;
        }

        public bool IsManager(int id)
        {
            return IdManagers.Contains(id);
        }
        public void ShowMenu(int worker_id)
        {
            string? user_option;

            do
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("1. List team members by team name");
                Console.WriteLine("2. List unassigned tasks");
                Console.WriteLine("3. List task assignments by team name");
                Console.WriteLine("4. Assign IT worker to a team as technician");
                Console.WriteLine("5. Assign task to IT worker");
                Console.WriteLine($"{exit_option}. Exit");
                Console.WriteLine("==============================================");
                Console.Write("Choose an option: ");
                user_option = Console.ReadLine();

                ProcessOption(user_option, worker_id);

                if (user_option != exit_option)
                {
                    string? user_cont;
                    Console.WriteLine("Do you want to do any other operation?");
                    Console.WriteLine("(Y)es or (N)o");
                    user_cont = Console.ReadLine();

                    if ((user_cont == "N") || (user_cont == "n")) return;
                }

            } while (user_option != exit_option);
        }

        public void ProcessOption(string option, int worker_id)
        {
            switch (option)
            {
                case "1":
                    {
                        ListTeamMembers(worker_id);
                        break;
                    }
                case "2":
                    {
                        ListUnassignedTasks(worker_id);
                        break;
                    }
                case "3":
                    {
                        ListTaskAssignementsTeam(worker_id);
                        break;
                    }
                case "4":
                    {
                        SetTeamTechnician();
                        break;
                    }
                case "5":
                    {
                        SetWorkerToTask(worker_id);
                        break;
                    }
                case exit_option:
                    {
                        break;
                    }
                default:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
            }
        }
        public void RegisterITWorker()
        {

        }
        public void ListITWorker()
        {

        }
        public void RegisterTeam()
        {

        }
        public void RegisterTask()
        {

        }
        public void ListTeams()
        {

        }
        public void ListTeamMembers(int id)
        {
            int NTeams = Admin.GetNTeams();
            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine("Teams");
            Console.WriteLine("========================");
            for (int i = 0; i < NTeams; i++)
            {
                Team team = Admin.GetTeam(i);

                if (id == team.TeamManager.Id)
                {
                    Console.WriteLine(team.Name);
                    Console.WriteLine("------------------------");
                    Admin.ListMembers(team);
                    Console.WriteLine("------------------------");
                }
            }
            Console.WriteLine("========================");
        }
        public void ListUnassignedTasks(int worker_id)
        {
            Admin.ListUnassignedTasks(worker_id);
        }
        public void ListTaskAssignementsTeam(int id)
        {
            int NTeams = Admin.GetNTeams();
            int NTasks = Admin.GetNAssTasks();
            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("List Assigned tasks");
            Console.WriteLine("=======================");
            for (int i = 0; i < NTeams; i++)
            {
                Team team = Admin.GetTeam(i);
                if (team.TeamManager.Id == id)
                {
                    Console.WriteLine($"{team.Name}");
                    Console.WriteLine("------------------------");
                    List<ITWorker> techs = team.Technicians;
                    for (int j = 0; j < techs.Count; j++)
                    {
                        for (int k = 0; k < NTasks; k++)
                        {
                            ITTask task = Admin.GetAssTask(k);
                            if (task.IdWorker == techs[j].Id)
                            {
                                Console.WriteLine($"{task.Name}");
                            }
                        }
                    }

                }

                Console.WriteLine("========================");
            }
            Console.WriteLine("========================");
        }
        public void SetTeamManager()
        {

        }
        public void SetTeamTechnician()
        {
            Admin.SetTeamTechnician();
        }
        public void SetWorkerToTask(int worker_id)
        {
            Admin.SetWorkerToTask(worker_id);
        }
        public void UnregisterWorker()
        {

        }
    }
}
