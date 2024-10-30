using MultiBankOOP.Infrastructure.Contracts;
using MultiBankOOP.Library.Contracts;
using MultiBankOOP.Library.Contracts.DTOs;
using MultiBankOOP.XCutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MultiBankOOP.Presentation.ConsoleUI
{
    internal class LoginMenu
    {
        private readonly IAccountService _accountService;
        private readonly MainMenu _mainMenu;

        public LoginMenu(IAccountService accountService, MainMenu mainMenu )
        {
            _accountService = accountService;
            _mainMenu = mainMenu;
        }
        public void Execute ()
        {
            string? accountNumber;
            string? pin;
            int     parsedPin;

            while (true)
            {
                Console.Clear();
                Console.Write("Enter your account number to login or 'exit' to close the app: ");
                accountNumber = Console.ReadLine();

                if (accountNumber == "exit" || accountNumber == "Exit")
                    return;

                Console.Write("Enter your account pin: ");
                pin = Console.ReadLine();

                bool isPinInt = int.TryParse(pin, out parsedPin);
                if (!isPinInt)
                {
                    Console.WriteLine("Entered Pin is not a valid integer");
                    Console.WriteLine("Press aby key to retry.");
                    Console.ReadKey();
                    continue;
                }

                LoginResultDto loginResultInfo = _accountService.Login(accountNumber,parsedPin);
                if (loginResultInfo.ResultHasErrors)
                {
                    ManageLoginError(loginResultInfo);
                    Console.Write("Press any key to retry: ");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    Console.Write("Login Successfully, press any key to enter the main menu");
                    Console.ReadKey();
                    _mainMenu.UserNumber = accountNumber;
                    _mainMenu.Execute();
                }

            }

        }

        public void ManageLoginError(LoginResultDto loginResultInfo)
        {
            if (loginResultInfo.Error == LoginErrorEnum.IncorrectPin)
                Console.WriteLine("Incorrect Pin.");
            else
                Console.WriteLine("User not found");
        }
    }
}
