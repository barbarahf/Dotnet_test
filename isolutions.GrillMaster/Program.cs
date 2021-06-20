using isolutions.GrillMaster.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace isolutions.GrillMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = BuildAndGetServiceProvider();
            
            var grillMasterResolver = serviceProvider.GetService<IGrillMasterResolver>();

            grillMasterResolver?.Resolve().Wait();

            Console.ReadKey();
        }

        private static ServiceProvider BuildAndGetServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IGrillMenuService, GrillMenuService>()
                .AddSingleton<IGrillMasterResolver, GrillMasterResolver>()
                .AddHttpClient()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}