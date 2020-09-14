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
using Newtonsoft.Json;
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
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceCollection = new ServiceCollection();                // start old
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();
            services.AddScoped<IPetTypeService, PetTypeService>();

            services.AddScoped<IFakeDB, FakeDB>();  // Needed??

            services.AddControllers(); /* o.AddNetwtonsoftJson(option =>
            {option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }(;  */
            services.AddMvc().AddNewtonsoftJson();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);  //MAYBE
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Use the default property (Pascal) casing
               options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               options.SerializerSettings.MaxDepth = 2;
            });

            // Build provider
            //var serviceProvider = services.BuildServiceProvider();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var petrepo = scope.ServiceProvider.GetService<IPetRepository>();
                    var ownerrepo = scope.ServiceProvider.GetService<IOwnerRepository>();
                    var petTyperepo = scope.ServiceProvider.GetService<IPetTypeRepository>();
                    new FakeDB(ownerrepo, petTyperepo, petrepo).InitData();
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
