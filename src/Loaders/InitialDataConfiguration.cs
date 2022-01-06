
using NGroot;
using ShipmentsApi;

namespace ShipmentsApi
{
    public static class InitialDataConfiguration
    {
        public static async Task ConfigureInitialData(
            this IServiceProvider provider,
            IWebHostEnvironment env
        )
        {
            var loaders = new List<Type> {
                typeof(IPackagesLoader),
            };
            var testLoaders = new List<Type>
            { };
            var masterLoader = new MasterLoader<SeedData>(loaders, testLoaders);
            await masterLoader.ConfigureInitialData(provider, env.ContentRootPath);
        }
    }
}