using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Schema;

namespace FamilyLibrary
{
    public class Son : Father
    {
        public string SchoolName { get; set; }
        private string SchoolYear { get; set; }
        protected string SchoolId { get; set; }

        public Son()
        {
            Name = "Rob";
            Age = "12";
            // Surname = "Michaels"; Como el atributo es privado no se puede acceder/modificar desde otra clase
            Job = "None";
            // CompanyName = "None"; Igual que antes, como el atributo es privado no se puede acceder/modificar desde otra clase
            NumberOfSons = "0";
            SchoolName = "Gran Canyon Elementary Schoool";
            SchoolYear = "6th";
            SchoolId = "12345";
        }
        public void ShowValues()
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("The values are the following ones:");
            Console.WriteLine("==================================");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Job: {Job}");
            Console.WriteLine($"Number of sons: {NumberOfSons}");
            Console.WriteLine($"School name: {SchoolName}");
            Console.WriteLine($"School year: {SchoolYear}");
            Console.WriteLine($"School Id: {SchoolId}");
            Console.WriteLine("==================================");
        }

        public void ModifyAttributes()
        {
            bool user_continue = true;
            List<string> attributes = new List<string>();

            attributes.Add("name");
            attributes.Add("age");
            attributes.Add("job");
            attributes.Add("number of sons");
            attributes.Add("school name");
            attributes.Add("school year");
            attributes.Add("school id");

            for (int i = 0; i < attributes.Count; i++)
            {
                if (AskToModify(attributes[i]))
                {
                    string? new_value;

                    new_value = GetNewValue();

                    if (attributes[i] == "name")
                        Name = new_value;
                    else if (attributes[i] == "age")
                        Age = new_value;
                    else if (attributes[i] == "job")
                        Job = new_value;
                    else if (attributes[i] == "number of sons")
                        NumberOfSons = new_value;
                    else if (attributes[i] == "school name")
                        SchoolName = new_value;
                    else if (attributes[i] == "school year")
                        SchoolYear = new_value;
                    else
                        SchoolId = new_value;

                    ShowSuccessInSet(attributes[i], new_value);
                }
                else
                {
                    ShowNotSet();
                }

                user_continue = AskContinue();
                if (!user_continue)
                {
                    break;

                }
            }

            ShowValues();
        }

        private bool AskToModify(string attr_modify)
        {
            string? user_option;

            Console.Clear();
            Console.WriteLine($"Do you want to modify the {attr_modify} (Y)es or (N)o)? ");
            user_option = Console.ReadLine();

            if ((user_option == "y") || (user_option == "Y")) return true;

            return false;
        }
        private string GetNewValue()
        {
            string? new_value;
            Console.Write("Enter the new value: ");
            new_value = Console.ReadLine();

            return new_value;
        }

        private void ShowNotSet()
        {
            Console.WriteLine("Attribute not set");
        }

        private void ShowSuccessInSet(string option, string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Set {option} successfully: {value} ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private bool AskContinue()
        {
            string? user_option;

            Console.WriteLine("Press any key to continue or 'end' to end modification");
            user_option = Console.ReadLine();

            if (user_option == "end") return false;

            return true;
        }
    }
}
