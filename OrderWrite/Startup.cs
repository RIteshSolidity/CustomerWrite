using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using OrderWrite.Repository;
using OrderWrite.Models;
using OrderWrite.Commands;
using OrderWrite.Events;
using OrderWrite.Services;
using OrderWrite.Infrastructure;

namespace OrderWrite
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<OrderDBContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("OrdersWritecs")));
            services.AddScoped<IOrderRepository, OrdersRepository>();
            services.AddScoped<ICommandHandler, CommandHandlers>();
            services.AddScoped<IServicebusSender, ServiceBusSender>(
                aa => new ServiceBusSender(
                        Configuration.GetConnectionString("ServiceBuscs"),
                        Configuration.GetValue<string>("TopicName")
                    ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
