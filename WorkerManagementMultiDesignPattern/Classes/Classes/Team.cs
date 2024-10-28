using Classes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Classes
{
    public class Team
    {
        public string Name { get; set; }
        public ITWorker TeamManager { get; set; }
        public List<ITWorker> Technicians { get; set; }

        public bool HasManager { get; set; }

        public Team(string name)
        {
            Name = name;
            Technicians = new List<ITWorker>();
            HasManager = false;
        }

        public void SetManager(ITWorker manager)
        {
            TeamManager = manager;
            HasManager = true;
        }

        public void SetTechnician(ITWorker technician)
        {
            Technicians.Add(technician);
        }

        public bool DeleteWorkerFromTeam(ITWorker worker)
        {
            for (int i = 0; i < Technicians.Count; ++i)
            {
                if (Technicians[i] == worker)
                {
                    Technicians.Remove(worker);
                    return true;
                }
            }

            return false;
        }
    }
}
