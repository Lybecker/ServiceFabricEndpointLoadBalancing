namespace WebService.Model
{
    public class ServiceHealthStatus
    {
        public ServiceHealthStatus()
        {
            IsShuttingDown = false;
        }

        /// <summary>
        /// The service is shutting down and will soon stop service requests.
        /// </summary>
        public bool IsShuttingDown { get; set; }
    }
}
