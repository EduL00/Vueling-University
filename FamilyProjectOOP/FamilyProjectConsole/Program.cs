using System;
using System.Reflection;
using FamilyLibrary;

string?      user_option;
const string exit_option = "3";
var          error_color = ConsoleColor.Red;
var          font_color = ConsoleColor.White;
Son          user = new();

do
{
    Console.Clear();
    Console.WriteLine("====================================");
    Console.WriteLine("1. Show Values");
    Console.WriteLine("2. Modify Values");
    Console.WriteLine($"{exit_option}. Exit");
    Console.WriteLine("====================================");
    Console.Write("Choose an action: ");
    user_option = Console.ReadLine();

    switch (user_option)
    {
        case "1":
            {
                user.ShowValues();
                break;
            }
        case "2":
            {
                user.ModifyAttributes();
                break;
            }
        case exit_option:
            {
                return;
            }
        default:
            {
                Console.ForegroundColor = error_color;
                Console.WriteLine("Invalid option\n");
                Console.ForegroundColor = font_color;
                break;
            }
    }

    if (user_option != exit_option)
    {
        string? user_cont;
        Console.WriteLine("");
        Console.WriteLine("Do you want to do any other action?");
        Console.WriteLine("(Y)es or (N)o");
        user_cont = Console.ReadLine();

        if ((user_cont == "N") || (user_cont == "n")) return;
    }

} while (user_option != exit_option);