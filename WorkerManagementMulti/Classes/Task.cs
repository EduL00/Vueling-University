using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public enum TaskStatus
    {
        ToDo = 1,
        Doing = 2,
        Done = 3
    }
    public class Task
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Technology { get; set; }
        public TaskStatus Status { get; set; }
        public int IdWorker { get; set; }

        public Task(int id, string name, string description, string technology, TaskStatus status)
        {
            Id = id;
            Name = name;
            Description = description;
            Technology = technology;
            Status = status;
        }

        public void ShowUnassignedTask()
        {
            string status;

            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description:{Description}");
            Console.WriteLine($"Technology: {Technology}");
            status = GetStringStatus(Status);
            Console.WriteLine($"Status: {Status}");
        }

        private string GetStringStatus(TaskStatus status)
        {
            if (status == TaskStatus.Done) return "Done";
            else if (status == TaskStatus.ToDo) return "To do";

            return "Doing";
        }

        public void SetWorker(int id)
        {
            IdWorker = id;
        }

        public bool DeleteWorkerFromTask(ITWorker worker)
        {
            if (worker.Id == IdWorker)
            {
                Id = -1;
                return true;
            }

            return false;
        }
    }
}
