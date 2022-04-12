using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NGroot;
using ShipmentsApi.Models;

namespace ShipmentsApi
{
    public interface IShipmentsLoader : IModelLoader
    { }
    public class ShipmentsLoader : ModelLoader<Shipment, InitialData>, IShipmentsLoader
    {
        public ShipmentsLoader(IOptions<NgrootSettings<InitialData>> settings, ShipmentsContext context) : base(settings)
        {
            Setup(InitialData.Shipments)
                .FindDuplicatesWith(m => context.Shipments.FirstOrDefaultAsync(shipment => shipment.Id == m.Id))
                .OverrideDuplicatesWith(async (model, duplicate) =>
                {
                    duplicate.SenderName = model.SenderName;
                    duplicate.SenderAddress = model.SenderAddress;
                    duplicate.Status = model.Status;
                    duplicate.EstimatedDeliveryDate = model.EstimatedDeliveryDate;
                    duplicate.DeliveryDate = model.DeliveryDate;
                    await context.SaveChangesAsync();
                    return duplicate;
                })
                .CreateModelUsing(async (m) =>
                {
                    context.Add(m);
                    await context.SaveChangesAsync();
                    return m;
                })
            .UseFileLoader();
        }

        protected override async Task<List<Shipment>> LoadModelAsync(Dictionary<string, object> collaborators)
        {
            var shipments = await base.LoadModelAsync(collaborators);

            shipments.ForEach(sh => sh.EstimatedDeliveryDate = DateTime.Today.AddDays(2));
            return shipments;
        }
    }
}