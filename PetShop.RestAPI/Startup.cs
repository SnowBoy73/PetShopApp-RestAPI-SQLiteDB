using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PetShop.Core.ApplicationService;
using PetShop.Core.ApplicationService.Impl;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using PetShop.Infrastructure.Data;
using PetShop.Infrastructure.Data.Repositories;
using PetShop.Infrastructure.SQL;
using PetShop.Infrastructure.SQL.Repositories;

namespace PetShop.RestAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }
        public object LoggerService { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //    var serviceCollection = new ServiceCollection();  //new

            /*   var loggerfactory = LoggerService.Create(builder => {
                   builder.AddConsole();
               });  */

            // if dev do this
           services.AddDbContext<PetShopContext>(
            opt => opt.UseSqlite("Data Source= petshopApp.db"));
            //: DbOptionsBuilder =>
                 //: DbOptionsBuilder =>
                /*   {
                       opt
                       UseLoggerFactory(loggerfactory)
                       .UseSqlite(connectionString: "Data Source = petapp.db");
                   } */
               
            // to here
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();

            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();
            services.AddScoped<IPetTypeService, PetTypeService>();
            services.AddScoped<IFakeDB, FakeDB>();

            services.AddControllers(); 
            services.AddMvc().AddNewtonsoftJson();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers().AddNewtonsoftJson(options =>
            {    // Use the default property (Pascal) casing
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             //   options.SerializerSettings.MaxDepth = 100;  // 100 pet limit per owner
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
                    var ctx = scope.ServiceProvider.GetService<PetShopContext>();
                  

                    DBInitialiser.SeedDB(ctx);
                    Console.WriteLine("pet count = " + ctx.Pets.Count());
                    Console.WriteLine("owner count = " + ctx.Owners.Count());
                    Console.WriteLine("pet type count = " + ctx.PetTypes.Count());

                    var petRepository = scope.ServiceProvider.GetService<IPetRepository>();
                    var ownerRepository = scope.ServiceProvider.GetService<IOwnerRepository>();
                    var petTypeRepository = scope.ServiceProvider.GetService<IPetTypeRepository>();
                   // new FakeDB(ownerrepo, petTyperepo, petrepo).InitData();

                //    new DBInitialiser(petRepository, ownerRepository, petTypeRepository).InitData();

                }
            }
            else
            {
                app.UseHsts();
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
