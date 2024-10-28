using Classes.Classes;
using Classes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Controllers
{
    internal class ControllerWorker : MenuInterface
    {
        const string exit_option = "4";
        ControllerAdmin Admin;
        ConsoleColor succ_color = ConsoleColor.Green;
        ConsoleColor err_color = ConsoleColor.Red;
        ConsoleColor std_color = ConsoleColor.White;//elc mirar lo de color

        public ControllerWorker(ControllerAdmin admin)
        {
            Admin = admin;
        }
        public void ShowMenu(int worker_id)
        {
            string? user_option;

            do
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("1  List unassigned tasks");
                Console.WriteLine("2. List task assignments by team name");
                Console.WriteLine("3. Assign task to IT worker");
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
                        ListUnassignedTasks(worker_id);
                        break;
                    }
                case "2":
                    {
                        ListTaskAssignementsTeam(worker_id);
                        break;
                    }
                case "3":
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
                        Console.ForegroundColor = err_color;
                        Console.WriteLine("Invalid option\n");
                        Console.ForegroundColor = std_color;
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

        }
        public void ListUnassignedTasks(int id)
        {
            Team team = null;
            ITWorker worker = Admin.GetITWorker(id);
            int NTeams = Admin.GetNTeams();

            if (worker.InATeam == false)
            {
                Console.WriteLine("Worker not in a team");
                return;
            }

            for (int i = 0; i < NTeams; ++i)
            {
                team = Admin.GetTeam(i);

                for (int j = 0; team.Technicians.Count < j; ++j)
                {
                    if ((team.Technicians[j].Id == worker.Id))
                        break;
                }

            }
            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine($"Tasks assigned to Team {team.Name}");
            Console.WriteLine("------------------------");
            for (int j = 0; j < team.Technicians.Count; ++j)
            {
                int NTasks = Admin.GetNAssTasks();

                for (int k = 0; k < NTasks; ++k)
                {
                    ITTask task = Admin.GetAssTask(k);
                    if (task.IdWorker == worker.Id)
                    {
                        Admin.ShowUnassignedTask(task);
                        Console.WriteLine("------------------------");
                    }
                }
            }
            Console.WriteLine("========================");

        }
        public void ListTaskAssignementsTeam(int id)
        {
            Team team = null;
            ITWorker worker = Admin.GetITWorker(id);
            int NTeams = Admin.GetNAssTasks();
            int NTasks = Admin.GetNAssTasks();

            if (worker.InATeam == false)
            {
                Console.WriteLine("Worker not in a team");
                return;
            }

            for (int i = 0; i < NTeams; ++i)
            {
                team = Admin.GetTeam(i);

                for (int j = 0; team.Technicians.Count < j; ++j)
                {
                    if ((team.Technicians[j].Id == worker.Id))
                        break;
                }

            }
            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine($"Tasks assigned to Team {team.Name}");
            Console.WriteLine("------------------------");
            for (int j = 0; j < team.Technicians.Count; ++j)
            {
                for (int k = 0; k < NTasks; ++k)
                {
                    ITTask task = Admin.GetAssTask(k);

                    if (task.IdWorker == worker.Id)
                    {
                        Admin.ShowUnassignedTask(task);
                        Console.WriteLine("------------------------");
                    }
                }
            }
            Console.WriteLine("========================");
        }
        public void SetTeamManager()
        {

        }
        public void SetTeamTechnician()
        {

        }
        public void SetWorkerToTask(int id)
        {
            bool found_task;
            ITTask task = null;
            ITWorker worker = Admin.GetITWorker(id);

            ListUnassignedTasks(id);

            if ((found_task = Admin.SearchTask(out task)) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Task not found");
                Console.ForegroundColor = std_color;
                return;
            }


            if ((found_task == true) && (task.Status == ITTaskStatus.Done))
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Task already Done");
                Console.ForegroundColor = std_color;
                return;
            }

            if ((found_task == true) && (worker.WorkerHasTech(task.Technology) == false))
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("The Worker has not the techkwnowledge to do the task");
                Console.ForegroundColor = std_color;
                return;
            }

            task.SetWorker(worker.Id);
            Admin.RemoveUnssignedTask(task);
            Admin.AddAssignedTask(task);

            Console.ForegroundColor = succ_color;
            Console.WriteLine($"Task {task.Id} assigned to IT Worker {worker.Id} successfully");
            Console.ForegroundColor = std_color;
            return;

        }
        public void UnregisterWorker()
        {

        }
    }
}
