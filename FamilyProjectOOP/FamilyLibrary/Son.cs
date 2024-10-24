using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (user_continue)
            {

                if (AskToModify("name"))
                {
                    Name = GetNewValue();
                    ShowSuccessInSet("name", Name);
                }
                else ShowNotSet();

                user_continue = AskContinue();
            }

            if (user_continue)
            {
                if (AskToModify("age"))
                {
                    Age = GetNewValue();
                    ShowSuccessInSet("age", Age);
                }
                else ShowNotSet();

                user_continue = AskContinue();
            }

            if (user_continue)
            {
                if (AskToModify("job"))
                {
                    Job = GetNewValue();
                    ShowSuccessInSet("job", Job);
                }
                else ShowNotSet();

                user_continue = AskContinue();
            }

            if (user_continue)
            {
                if (AskToModify("number of sons"))
                {
                    NumberOfSons = GetNewValue();
                    ShowSuccessInSet("number of sons", NumberOfSons);
                }
                else ShowNotSet();

                user_continue = AskContinue();
            }

            if (user_continue)
            {
                if (AskToModify("school name"))
                {
                    SchoolName = GetNewValue();
                    ShowSuccessInSet("school name", SchoolName);
                }
                else ShowNotSet();

                user_continue = AskContinue();
            }

            if (user_continue)
            {
                if (AskToModify("school year"))
                {
                    SchoolYear = GetNewValue();
                    ShowSuccessInSet("school year", SchoolYear);
                }
                else ShowNotSet();

                user_continue = AskContinue();
            }

            if (user_continue)
            {
                if (AskToModify("school id"))
                {
                    SchoolId = GetNewValue();
                    ShowSuccessInSet("school id", SchoolId);
                }
                else ShowNotSet();

                user_continue= AskContinue();
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
