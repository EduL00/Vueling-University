using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public enum ITWorkerLevel
    {
        Junior = 1,
        Medium = 2,
        Senior = 3
    }

     class ITWorker : Worker
    {
        public string YearsOfExperience {  get; set; }
        public List <string> TechKnowledge { get; set; }
        public ITWorkerLevel Level { get; set; }
        public bool InATeam { get; set; }

        public ITWorker (int id, string name, string surname, DateTime birth, string experience, ITWorkerLevel level, List<string> tech)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Birth = birth;
            YearsOfExperience = experience;
            Level = level;
            TechKnowledge = tech;
        }

        public bool WorkerHasTech (string tech)
        {
            for (int i = 0; i < TechKnowledge.Count; ++i)
            {
                if (TechKnowledge[i] == tech) return true;
            }

            return false;
        }

        public void ShowWorkerInfo ()
        {
            string? level = ParseWorkerLevelToString(Level);

            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Surname: {Surname}");
            Console.WriteLine($"Level: {level}");
            Console.WriteLine($"Birth Date: {Birth.Date.ToString("yyyy-MM-dd")}");
            Console.Write("Technologies: ");
            for (int i = 0;i < TechKnowledge.Count;i++)
            {
                Console.Write($"{TechKnowledge[i]}");
                if (i!= (TechKnowledge.Count-1))
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
    }
}
