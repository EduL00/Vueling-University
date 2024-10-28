using Classes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Classes.Classes
{
    public enum ITWorkerLevel
    {
        Junior = 1,
        Medium = 2,
        Senior = 3
    }

    public class ITWorker : Worker
    {
        public string YearsOfExperience { get; set; }
        public List<string> TechKnowledge { get; set; }
        public ITWorkerLevel Level { get; set; }
        public bool InATeam { get; set; }
        public bool IsManager;

        public ITWorker(int id, string name, string surname, DateTime birth, string experience, ITWorkerLevel level, List<string> tech)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birth = birth;
            YearsOfExperience = experience;
            Level = level;
            TechKnowledge = tech;
            IsManager = false;
            InATeam = false;
        }

        public bool WorkerHasTech(string tech)
        {
            for (int i = 0; i < TechKnowledge.Count; ++i)
            {
                if (TechKnowledge[i] == tech) return true;
            }

            return false;
        }

        public void SetAsManager()
        {
            IsManager = true;
        }

        public void IsNowInATeam()
        {
            InATeam = true;
        }
    }
}
