using System.Dynamic;

namespace Classes.Classes
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }

        public DateTime LeavingDate { get; set; }

        private static int NITWorkers = 1;

        public int GetIdITWorker()
        {
            return NITWorkers;
        }

        public void IncrementIdITWorker()
        {
            NITWorkers++;
        }
    }
}