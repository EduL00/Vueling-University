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
                .AddScoped<LoginMenu>()
                .AddScoped<MainMenu>()
                .BuildServiceProvider();

            LoginMenu? login_menu = serviceProvider.GetService<LoginMenu>();

            login_menu?.Execute();
        }
    }
}