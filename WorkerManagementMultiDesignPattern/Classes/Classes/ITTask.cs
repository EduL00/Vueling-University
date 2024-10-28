using Classes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Classes
{
    public enum ITTaskStatus
    {
        ToDo = 1,
        Doing = 2,
        Done = 3
    }
    public class ITTask
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Technology { get; set; }
        public ITTaskStatus Status { get; set; }
        public int IdWorker { get; set; }

        public ITTask(int id, string name, string description, string technology, ITTaskStatus status)
        {
            Id = id;
            Name = name;
            Description = description;
            Technology = technology;
            Status = status;
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