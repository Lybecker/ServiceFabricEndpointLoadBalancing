using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton(new Model.ServiceHealthStatus());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime, Model.ServiceHealthStatus serviceHealthStatus)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // register the 
            applicationLifetime.ApplicationStopping.Register(() => OnShutdown(serviceHealthStatus));

            app.UseMvc();
        }

        private void OnShutdown(Model.ServiceHealthStatus serviceHealthStatus)
        {
            ServiceEventSource.Current.Message($"Shutdown process starting. Node: {Environment.GetEnvironmentVariable("HostedServiceName")}");

            serviceHealthStatus.IsShuttingDown = true;

            System.Threading.Thread.Sleep(10000);

            ServiceEventSource.Current.Message($"Shutdown process complete. Node: {Environment.GetEnvironmentVariable("HostedServiceName")}");
        }
    }
}
