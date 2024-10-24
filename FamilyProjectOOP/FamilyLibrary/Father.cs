using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyLibrary
{
    public class Father : GrandFather
    {
        public string Job {  get; set; }
        private string CompanyName { get; set; }
        protected string NumberOfSons { get; set; }
    }
}
