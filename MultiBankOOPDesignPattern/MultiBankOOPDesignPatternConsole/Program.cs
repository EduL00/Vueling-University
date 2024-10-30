using Microsoft.Extensions.DependencyInjection;
using MultiBankOOP.Infrastructure.Contracts;
using MultiBankOOP.Library.Contracts;
using MultiBankOOP.Library.Impl;
using MultiBankOOP.Infrastructure.Impl;

namespace MultiBankOOP.Presentation.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            ServiceProvider serviceProvider = services
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<IMovementsRepository, MovementsRepository>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<MainMenu>()
                .BuildServiceProvider();

            MainMenu? mm = serviceProvider.GetService<MainMenu>();

            mm?.Execute();
        }
    }
}