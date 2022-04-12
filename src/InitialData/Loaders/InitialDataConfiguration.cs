
using NGroot;

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
                typeof(ShipmentsLoader),
                typeof(PackagesLoader),
            };
            var testLoaders = new List<Type>
            { };
            var masterLoader = new MasterLoader<InitialData>(loaders, testLoaders);
            await masterLoader.ConfigureInitialData(provider, env.ContentRootPath);
        }
    }
}