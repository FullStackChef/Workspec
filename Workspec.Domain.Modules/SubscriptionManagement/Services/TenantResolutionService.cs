namespace Workspec.Domain.Modules.SubscriptionManagement.Services
{
    public interface ITenantResolutionService
    {
        Guid GetTenantId();
    }
    public class TenantResolutionService : ITenantResolutionService
    {
        public Guid GetTenantId()
        {
            return new Guid("a56a6840-28ec-411a-a4bd-53ba95ffae65");
        }
    }
}
