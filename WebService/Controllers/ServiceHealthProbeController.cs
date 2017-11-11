using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    public class ServiceHealthProbeController : Controller
    {
        private readonly Model.ServiceHealthStatus _serviceHealthProbe;

        public ServiceHealthProbeController(Model.ServiceHealthStatus serviceHealthProbe)
        {
            _serviceHealthProbe = serviceHealthProbe;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (_serviceHealthProbe.IsShuttingDown == false)
                return Ok("Everything is running smooth");
            else
                return StatusCode(503, "Service terminating - stop sending requests");
        }
    }
}