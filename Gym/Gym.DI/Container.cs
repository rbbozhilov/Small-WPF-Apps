using Gym.Services.Client;
using Microsoft.Extensions.DependencyInjection;


namespace Gym.DI
{
    public class Container
    {
        public static ServiceProvider ConfigureServices()
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IClientService, ClientService>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
