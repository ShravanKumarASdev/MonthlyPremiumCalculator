using Microsoft.EntityFrameworkCore;
using MonthlyPremiumCalculator.API.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremiumCalculator.API
{
    //Inmemory DbContext
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options): base(options) { }

        public DbSet<OccupationRating> OccupationRatings { get; set; }
        public DbSet<RatingFactor> RatingFactors { get; set; }
    }
}
