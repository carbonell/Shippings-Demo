using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NGroot;
using ShipmentsApi.Models;

namespace ShipmentsApi
{
    public interface IPackagesLoader : IModelLoader
    { }
    public class PackagesLoader : ModelLoader<Package, SeedData>, IPackagesLoader
    {
        public PackagesLoader(IOptions<NgrootSettings<SeedData>> settings, ShipmentsContext context) : base(settings)
        {
            Setup(SeedData.Packages)
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