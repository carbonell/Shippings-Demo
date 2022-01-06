using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NGroot;
using ShipmentsApi.Models;

namespace ShipmentsApi
{
    public interface IPackagesLoader : IModelLoader
    { }
    public class PackagesLoader : ModelLoader<Package, InitialData>, IPackagesLoader
    {
        public PackagesLoader(IOptions<NgrootSettings<InitialData>> settings, ShipmentsContext context) : base(settings)
        {
            Setup(InitialData.Packages)
                .FindDuplicatesWith(m => context.Packages.FirstOrDefaultAsync(pck => pck.Name == m.Name))
                .CreateModelUsing(async (m) =>
                {
                    context.Add(m);
                    await context.SaveChangesAsync();
                    return m;
                });
        }
    }
}