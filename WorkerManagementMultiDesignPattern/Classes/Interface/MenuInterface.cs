using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Interface
{
    public interface MenuInterface
    {
        public void ShowMenu(int worker_id);

        public void ProcessOption(string option, int worker_id);
        public void RegisterITWorker();
        public void ListITWorker();
        public void RegisterTeam();
        public void RegisterTask();
        public void ListTeams();
        public void ListTeamMembers(int worker_id);
        public void ListUnassignedTasks(int worker_id);
        public void ListTaskAssignementsTeam(int worker_id);
        public void SetTeamManager();
        public void SetTeamTechnician();
        public void SetWorkerToTask(int worker_id);
        public void UnregisterWorker();

    }
}
