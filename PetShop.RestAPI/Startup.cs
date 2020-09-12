using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetShop.Core.ApplicationService;
using PetShop.Core.ApplicationService.Impl;
using PetShop.Core.DomainService;
using PetShop.Infrastructure.Data.Repositories;

namespace PetShop.RestAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           // FakeDB fakeDB;  // NEW probably very bad
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceCollection = new ServiceCollection();                // start old
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddControllers(); /* o.AddNetwtonsoftJson(option =>
            {option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }(;  */
            services.AddScoped<IFakeDB, FakeDB>();  // Needed??
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);  //MAYBE

            // Build provider
            var serviceProvider = services.BuildServiceProvider();
           // var fakeDB = serviceProvider.GetRequiredService<IFakeDB>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var repo = scope.ServiceProvider.GetService<IPetRepository>();
                    new FakeDB(repo).InitData();
                }
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // localhost/pet
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
