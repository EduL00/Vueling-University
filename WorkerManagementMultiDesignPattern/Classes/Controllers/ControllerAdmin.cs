using Classes.Classes;
using Classes.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Classes.Controllers
{
    public class ControllerAdmin : MenuInterface
    {
        ConsoleColor succ_color = ConsoleColor.Green;
        ConsoleColor err_color = ConsoleColor.Red;
        ConsoleColor std_color = ConsoleColor.White;//elc mirar lo de color
        private const string exit_option = "13";
        private List<ITWorker> Workers;
        private List<ITTask> Unassignedtasks;
        private List<ITTask> AssignedTasks;
        private List<Team> Teams;
        private Worker Worker;
        private int LastTakId;

        public ControllerAdmin()
        {
            Workers = new();
            Unassignedtasks = new();
            AssignedTasks = new();
            Teams = new();
            Worker = new();
            LastTakId = 0;
        }
        public void ShowMenu(int worker_id)
        {
            string? user_option;

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
                        RegisterITWorker();
                        break;
                    }
                case "2":
                    {
                        ListITWorker();
                        break;
                    }
                case "3":
                    {
                        RegisterTeam();
                        break;
                    }
                case "4":
                    {
                        RegisterTask();
                        break;
                    }
                case "5":
                    {
                        ListTeams();
                        break;
                    }
                case "6":
                    {
                        ListTeamMembers(worker_id);
                        break;
                    }
                case "7":
                    {
                        ListUnassignedTasks(worker_id);
                        break;
                    }
                case "8":
                    {
                        ListTaskAssignementsTeam(worker_id);
                        break;
                    }
                case "9":
                    {
                        SetTeamManager();
                        break;
                    }
                case "10":
                    {
                        SetTeamTechnician();
                        break;
                    }
                case "11":
                    {
                        SetWorkerToTask(worker_id);
                        break;
                    }
                case "12":
                    {
                        UnregisterWorker();
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
            string? name;
            string? surname;
            string? experience;
            string? level;
            string? tech;
            int worker_id;
            ITWorkerLevel enum_level;
            List<string> techlknowledge = new();
            DateTime birth;
            DateTime now = DateTime.Now;

            Console.Clear();
            Console.WriteLine("Enter the IT worker name: ");
            name = Console.ReadLine();
            Console.WriteLine("Enter the IT worker surname: ");
            surname = Console.ReadLine();
            Console.WriteLine("Enter the IT worker birth date (yyyy-mm-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out birth) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Invalid Date");
                Console.WriteLine("IT Worker not registered");
                Console.ForegroundColor = std_color;
                return;
            }
            if (now.Year - birth.Year < 18)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("The ITWorker must have at least 18 years old");
                Console.WriteLine("IT Worker not registered");
                Console.ForegroundColor = std_color;
                return;
            }
            Console.WriteLine("Enter the IT worker experience years: ");
            experience = Console.ReadLine();
            Console.WriteLine("Enter the IT worker level (1 = Junior, 2 = Medium and 3 = Senior): ");
            level = Console.ReadLine();
            if (ParseWorkerLevel(level, out enum_level) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Invalid worker level");
                Console.WriteLine("IT Worker not registered");
                Console.ForegroundColor = std_color;
                return;
            }
            if ((enum_level == ITWorkerLevel.Senior) && (int.Parse(experience) < 5))
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("To be Senior, the IT worker has to have at least 5 years of experience");
                Console.WriteLine("IT Worker not registered");
                Console.ForegroundColor = std_color;
                return;
            }
            Console.WriteLine("Enter the technologies known by the IT worker (finish entering'end'): ");
            while ((tech = Console.ReadLine()) != "end")
            {
                if (tech == null)
                {
                    Console.ForegroundColor = err_color;
                    Console.WriteLine("Invalid technology");
                    Console.WriteLine("IT Worker not registered");
                    Console.ForegroundColor = std_color;
                    return;
                }

                techlknowledge.Add(tech);
            }


            worker_id = Worker.GetIdITWorker();
            ITWorker it_worker = new(worker_id, name, surname, birth, experience, enum_level, techlknowledge);
            Workers.Add(it_worker);
            Worker.IncrementIdITWorker();

            Console.ForegroundColor = succ_color;
            Console.WriteLine("IT Worker successfully added.");
            Console.ForegroundColor = std_color;
        }

        public bool ParseWorkerLevel(string level, out ITWorkerLevel enum_level)
        {
            if ((level == "1") || (level == "Junior") || (level == "junior"))
            {
                enum_level = ITWorkerLevel.Junior;
                return true;
            }
            else if ((level == "2") || (level == "Medium") || (level == "medium"))
            {
                enum_level = ITWorkerLevel.Medium;
                return true;
            }
            else if ((level == "3") || (level == "Senior" || (level == "senior")))
            {
                enum_level = ITWorkerLevel.Senior;
                return true;
            }

            enum_level = ITWorkerLevel.Junior;

            return false;
        }

        public void ListITWorker()
        {
            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine("IT workers");
            Console.WriteLine("========================");
            for (int i = 0; i < Workers.Count; i++)
            {
                ShowWorkerInfo(Workers[i]);
                Console.WriteLine("--------------------");
            }
        }
        public void ShowWorkerInfo(ITWorker worker)
        {
            string? level = ParseWorkerLevelToString(worker.Level);

            Console.WriteLine($"Id: {worker.Id}");
            Console.WriteLine($"Name: {worker.Name}");
            Console.WriteLine($"Surname: {worker.Surname}");
            Console.WriteLine($"Level: {level}");
            Console.WriteLine($"Birth Date: {worker.Birth.Date.ToString("yyyy-MM-dd")}");
            Console.Write("Technologies: ");
            for (int i = 0; i < worker.TechKnowledge.Count; i++)
            {
                Console.Write($"{worker.TechKnowledge[i]}");
                if (i != (worker.TechKnowledge.Count - 1))
                    Console.Write($",");

            }
            Console.WriteLine("");
        }

        private string ParseWorkerLevelToString(ITWorkerLevel enum_level)
        {
            if (enum_level == ITWorkerLevel.Junior) return "Junior";
            else if (enum_level == ITWorkerLevel.Medium) return "Medium";

            return "Senior";
        }
        public void RegisterTeam()
        {
            string? name;

            Console.Clear();
            Console.Write("Enter the team name: ");
            name = Console.ReadLine();

            if (name == null)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Invalid team name");
                Console.ForegroundColor = std_color;
                return;

            }

            Team team = new(name);
            Teams.Add(team);

            Console.ForegroundColor = succ_color;
            Console.WriteLine("Team successfully registered");
            Console.ForegroundColor = std_color;
        }
        public void RegisterTask()
        {
            string? name;
            string? task_desc;
            string? task_tech;

            Console.Clear();
            Console.Write("Enter task name: ");
            name = Console.ReadLine();
            if (name == null)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Invalid name description");
                Console.ForegroundColor = std_color;
                return;
            }
            Console.WriteLine("Enter the task description: ");
            task_desc = Console.ReadLine();
            if (task_desc == null)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Invalid task description");
                Console.ForegroundColor = std_color;
                return;
            }

            Console.WriteLine("Enter the task techology: ");
            task_tech = Console.ReadLine();
            if (task_tech == null)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Invalid task technology");
                Console.ForegroundColor = std_color;
                return;
            }

            ITTask task = new(LastTakId, name, task_desc, task_tech, ITTaskStatus.ToDo);
            Unassignedtasks.Add(task);
            ++LastTakId;

            Console.ForegroundColor = succ_color;
            Console.WriteLine("Task successfully registered");
            Console.ForegroundColor = std_color;
        }
        public void ListTeams()
        {
            if (Teams.Count == 0)
            {
                Console.WriteLine("There are no teams registered");
                return;
            }
            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine("Team Names");
            Console.WriteLine("========================");
            for (int i = 0; i < Teams.Count; i++)
            {
                Console.WriteLine(Teams[i].Name);
            }
            Console.WriteLine("========================");

        }
        public void ListTeamMembers(int worker_id)
        {
            if (Teams.Count == 0)
            {
                Console.WriteLine("There are no teams registered");
                return;
            }
            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine("Teams");
            Console.WriteLine("========================");
            for (int i = 0; i < Teams.Count; i++)
            {

                Console.WriteLine(Teams[i].Name);
                Console.WriteLine("------------------------");
                ListMembers(Teams[i]);
                Console.WriteLine("------------------------");
            }
            Console.WriteLine("========================");

        }

        public void ListMembers(Team team)
        {
            if (team.HasManager == false)
            {
                Console.WriteLine("Team Manager: Not Set");
            }
            else
            {
                Console.WriteLine($"Team Manager: {team.TeamManager.Name} {team.TeamManager.Surname}");
            }

            if (team.Technicians.Count == 0)
            {
                Console.WriteLine("The team has no members");
                return;
            }

            Console.WriteLine("Team Member");

            for (int i = 0; i < team.Technicians.Count; i++) Console.WriteLine($"{i + 1}. {team.Technicians[i].Name} {team.Technicians[i].Surname}");

        }
        public void ListUnassignedTasks(int worker_id)
        {
            if (Unassignedtasks.Count == 0)
            {
                Console.WriteLine("There is no unassigned task.");
                return;
            }

            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine("Tasks");
            Console.WriteLine("------------------------");
            for (int i = 0; i < Unassignedtasks.Count; i++)
            {
                ShowUnassignedTask(Unassignedtasks[i]);
                Console.WriteLine("------------------------");
            }

        }

        public void ShowUnassignedTask(ITTask task)
        {
            string status;

            Console.WriteLine($"ID: {task.Id}");
            Console.WriteLine($"Name: {task.Name}");
            Console.WriteLine($"Description:{task.Description}");
            Console.WriteLine($"Technology: {task.Technology}");
            status = GetStringStatus(task.Status);
            Console.WriteLine($"Status: {status}");
        }

        private string GetStringStatus(ITTaskStatus status)
        {
            if (status == ITTaskStatus.Done) return "Done";
            else if (status == ITTaskStatus.ToDo) return "To do";

            return "Doing";
        }

        public void ListTaskAssignementsTeam(int worker_id)
        {
            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("List Assigned tasks");
            Console.WriteLine("=======================");
            for (int i = 0; i < Teams.Count; i++)
            {

                Console.WriteLine($"{Teams[i].Name}");
                Console.WriteLine("------------------------");
                List<ITWorker> techs = Teams[i].Technicians;
                for (int j = 0; j < techs.Count; j++)
                {
                    for (int k = 0; k < AssignedTasks.Count; k++)
                    {
                        if (AssignedTasks[k].IdWorker == techs[j].Id)
                        {
                            Console.WriteLine($"{AssignedTasks[k].Name}");
                        }
                    }
                }

                Console.WriteLine("========================");
            }
            Console.WriteLine("========================");
        }
        public void SetTeamManager()
        {
            string? id;
            string? name;
            bool found = false;
            bool found_team = false;
            ITWorker worker = null;
            Team team = null;

            ListITWorker();

            if ((found = SearchWorker(out worker)) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Worker not found");
                Console.ForegroundColor = std_color;
                return;
            }

            if ((found) && (worker.Level != ITWorkerLevel.Senior))
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("The Team Manages has to be Senior");
                Console.ForegroundColor = std_color;
                return;
            }

            ListTeams();

            if (found_team = SearchTeam(out team) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Team not found");
                Console.ForegroundColor = std_color;
                return;
            }

            team.SetManager(worker);
            worker.SetAsManager();

            Console.ForegroundColor = succ_color;
            Console.WriteLine($"Worker {worker.Id} set as manager of team {team.Name}");
            Console.ForegroundColor = std_color;
        }

        private bool SearchWorker(out ITWorker worker)
        {
            string? id;

            worker = null;
            Console.Write("Enter the ID of the IT worker: ");
            id = Console.ReadLine();

            if (id == null)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Not valid Id");
                Console.ForegroundColor = std_color;
                return false;
            }

            for (int i = 0; i < Workers.Count; i++)
            {
                if (Workers[i].Id == int.Parse(id))
                {
                    worker = Workers[i];
                    return true;
                }
            }

            return false;
        }

        public bool SearchTask(out ITTask task)
        {
            string? task_id;

            task = null;

            Console.Write("Enter the ID of the task to assign: ");
            task_id = Console.ReadLine();

            if (task_id == null)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Not valid Id");
                Console.ForegroundColor = std_color;
                return false;
            }

            for (int j = 0; j < Unassignedtasks.Count; ++j)
            {
                if (Unassignedtasks[j].Id == int.Parse(task_id))
                {
                    task = Unassignedtasks[j];
                    return true;
                }
            }

            return false;
        }

        private bool SearchTeam(out Team team)
        {
            string? team_name;

            team = null;

            Console.Write("Enter the name of the team: ");
            team_name = Console.ReadLine();

            if (team_name == null)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Not valid name");
                Console.ForegroundColor = std_color;
            }

            for (int j = 0; j < Teams.Count; j++)
            {
                if (Teams[j].Name == team_name)
                {
                    team = Teams[j];
                    return true;
                }
            }

            return false;

        }
        public void SetTeamTechnician()
        {
            string? id;
            string? name;
            bool found = false;
            bool found_team = false;
            ITWorker worker = null;
            Team team = null;

            ListITWorker();

            if ((found = SearchWorker(out worker)) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Worker not found");
                Console.ForegroundColor = std_color;
                return;
            }

            ListTeams();

            if (found_team = SearchTeam(out team) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Team not found");
                Console.ForegroundColor = std_color;
                return;
            }

            team.SetTechnician(worker);
            worker.IsNowInATeam();

            Console.ForegroundColor = succ_color;
            Console.WriteLine($"Worker {worker.Id} set as technician of team {team.Name}");
            Console.ForegroundColor = std_color;

        }
        public void SetWorkerToTask(int worker_id)
        {
            string? id;
            string? task_id;
            bool found = false;
            bool has_tec = false;
            bool found_task = false;
            bool task_done = false;
            ITWorker worker = null;
            ITTask task = null;

            ListITWorker();

            if ((found = SearchWorker(out worker)) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("IT Worker not found");
                Console.ForegroundColor = std_color;
                return;
            }

            ListUnassignedTasks(worker_id);

            if ((found_task = SearchTask(out task)) == false)
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
            Unassignedtasks.Remove(task);
            AssignedTasks.Add(task);

            Console.ForegroundColor = succ_color;
            Console.WriteLine($"Task {task.Id} assigned to IT Worker {worker.Id} successfully");
            Console.ForegroundColor = std_color;
            return;
        }
        public void UnregisterWorker()
        {
            string? id;
            bool found = false;
            ITWorker worker = null;

            ListITWorker();

            if ((found = SearchWorker(out worker)) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Worker not found");
                Console.ForegroundColor = std_color;
                return;
            }

            Workers.Remove(worker);
            if (worker.InATeam)
            {
                for (int i = 0; i < Teams.Count; i++)
                {
                    if (Teams[i].DeleteWorkerFromTeam(worker))
                        break;
                }
            }

            for (int i = 0; i < AssignedTasks.Count; ++i)
            {
                if (AssignedTasks[i].DeleteWorkerFromTask(worker))
                {
                    AssignedTasks.Remove(AssignedTasks[i]);
                    Unassignedtasks.Add(AssignedTasks[i]);
                    break;
                }
            }

            Console.ForegroundColor = succ_color;
            Console.WriteLine($"Worker {worker.Id} successfully unregistered.");
            Console.ForegroundColor = std_color;

        }

        public int GetNTeams()
        {
            return Teams.Count;
        }

        public Team GetTeam(int i)
        {
            return Teams[i];
        }

        public int GetNAssTasks()
        {
            return AssignedTasks.Count;
        }

        public ITTask GetAssTask(int i)
        {
            return AssignedTasks[i];
        }

        public ITWorker GetITWorker(int id)
        {
            for (int i = 0; i < Workers.Count; ++i)
            {
                if (Workers[i].Id == id) return Workers[i];
            }

            return null;
        }

        public void RemoveUnssignedTask(ITTask task)
        {
            Unassignedtasks.Remove(task);
        }

        public void AddAssignedTask(ITTask task)
        {
            AssignedTasks.Add(task);
        }
    }
}