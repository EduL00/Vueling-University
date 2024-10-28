using Classes.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Controllers
{
    public class ControllerLogin
    {
        ControllerAdmin Admin;
        ControllerManager Manager;
        ControllerWorker Worker;

        public ControllerLogin()
        {
            Admin = new ControllerAdmin();
            Manager = new ControllerManager(Admin);
            Worker = new ControllerWorker(Admin);
        }

        public void ShowLogin(string id)
        {

            if (id == "0")
            {
                Admin.ShowMenu(int.Parse(id));
            }
            else
            {
                if (Manager.IsManager(int.Parse(id)))
                    Manager.ShowMenu(int.Parse(id));
                else
                    Worker.ShowMenu(int.Parse(id));
            }
        }
    }
}
