using Microsoft.AspNetCore.SignalR;

namespace CodeHunt.Api.NotificationServices
{
    public class NotificationService<T> where T : Hub
    {
        protected IHubContext<T> HubContext { get; }

        public NotificationService(IHubContext<T> hubContext) 
        {
            HubContext = hubContext;
        }
    }
}
