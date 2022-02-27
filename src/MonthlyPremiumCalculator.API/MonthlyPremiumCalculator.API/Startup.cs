using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MonthlyPremiumCalculator.API.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremiumCalculator.API
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
            //CorsPlocy for SPA and API interaction
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            //EFCore Inmemory context
            services.AddDbContext<ApiContext>(option => option.UseInMemoryDatabase("MonthlyPremiumCalculationApp"));

            //Added to resolve data mapping issues between SPA and API
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Using Inmeory ApiContext for Occupation ratings and rating factors
            var context = serviceProvider.GetService<ApiContext>();
            AddTestData(context);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddTestData(ApiContext context)
        {
            //Using Inmeory hardcoded values for Occupation ratings and rating factors
            context.OccupationRatings.AddRange(new List<OccupationRating>() { new OccupationRating { Occupation = "Cleaner", Rating = "Light Manual" },
            new OccupationRating { Occupation = "Doctor", Rating = "Professional" },
            new OccupationRating { Occupation = "Author", Rating = "White Collar" },
            new OccupationRating { Occupation = "Farmer", Rating = "Heavy Manual" },
            new OccupationRating { Occupation = "Mechanic", Rating = "Heavy Manual" },
            new OccupationRating { Occupation = "Florist", Rating = "Heavy Manual" }});

            context.RatingFactors.AddRange(new List<RatingFactor>() { new RatingFactor { Rating="Professional", Factor=1.0M },
            new RatingFactor { Rating="White Collar", Factor=1.25M },
            new RatingFactor { Rating="Light Manual", Factor=1.50M },
            new RatingFactor { Rating="Heavy Manual", Factor=1.75M } });

            context.SaveChanges();
        }
    }
}
