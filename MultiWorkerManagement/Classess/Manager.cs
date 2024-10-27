using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Classes
{
    public class Manager
    {
        ConsoleColor succ_color = ConsoleColor.Green;
        ConsoleColor err_color = ConsoleColor.Red;
        ConsoleColor std_color = ConsoleColor.White;//elc mirar lo de color

        private List<ITWorker> Workers;
        private List<Task> Unassignedtasks;
        private List<Task> AssignedTasks;
        private List<Team> Teams;
        private int LastTakId;
        private Worker Worker;

        public Manager()
        {
            Workers = new List<ITWorker>();
            Unassignedtasks = new List<Task>();
            AssignedTasks = new List<Task>();
            Teams = new List<Team>();
            Worker = new Worker();
            LastTakId = 0;
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

        public void ListITWorkers()
        {
            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine("IT workers");
            Console.WriteLine("========================");
            for (int i = 0; i < Workers.Count; i++)
            {
                Workers[i].ShowWorkerInfo();
                Console.WriteLine("--------------------");
            }
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

            Task task = new(LastTakId, name, task_desc, task_tech, TaskStatus.ToDo);
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

        public void ListTeamMembers()
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
                Teams[i].ListMembers();
                Console.WriteLine("------------------------");
            }
            Console.WriteLine("========================");
        }

        public void ListUnassignedTasks()
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
                Unassignedtasks[i].ShowUnassignedTask();
                Console.WriteLine("------------------------");
            }
        }

        public void ListTaskAssignementsTeam()
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
            }
        }

        public void SetTeamManager()
        {
            string? id;
            string? name;
            bool found = false;
            bool found_team = false;
            ITWorker worker = null;
            Team team = null;

            ListITWorkers();

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
            }

            team.SetManager(worker);

            Console.ForegroundColor = succ_color;
            Console.WriteLine($"Worker {worker.Id} set as manager of team {team.Name}");
            Console.ForegroundColor = std_color;


        }
        public void SetTeamTechnician()
        {
            string? id;
            string? name;
            bool found = false;
            bool found_team = false;
            ITWorker worker = null;
            Team team = null;

            ListITWorkers();

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

            Console.ForegroundColor = succ_color;
            Console.WriteLine($"Worker {worker.Id} set as technician of team {team.Name}");
            Console.ForegroundColor = std_color;


        }
        public void SetWorkerToTask()
        {
            string? id;
            string? task_id;
            bool found = false;
            bool has_tec = false;
            bool found_task = false;
            bool task_done = false;
            ITWorker worker = null;
            Task task = null;

            ListITWorkers();

            if ((found = SearchWorker(out worker)) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("IT Worker not found");
                Console.ForegroundColor = std_color;
                return;
            }

            ListUnassignedTasks();

            if ((found_task = SearchTask(out task)) == false)
            {
                Console.ForegroundColor = err_color;
                Console.WriteLine("Task not found");
                Console.ForegroundColor = std_color;
                return;
            }


            if ((found_task == true) && (task.Status == TaskStatus.Done))
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

        private bool SearchTask(out Task task)
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

        public void UnregisterWorker()
        {
            string? id;
            bool found = false;
            ITWorker worker = null;

            ListITWorkers();

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

    }
}
