using Microsoft.EntityFrameworkCore;
using MonthlyPremiumCalculator.API.Controllers;
using MonthlyPremiumCalculator.API.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MonthlyPremiumCalculator.API.Test
{
    public class OccupationsControllerTest
    {
        private readonly OccupationsController _controller;
        private readonly ApiContext context;
        public OccupationsControllerTest()
        {
            //Using EFCore Inmemory database for mocking DbContext
            var options = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase("PremiumCalculatorDatabase").Options;

            context = new ApiContext(options);
            context.OccupationRatings.AddRange(new List<OccupationRating>() { new OccupationRating { Occupation = "Cleaner", Rating = "Light Manual" },
            new OccupationRating { Occupation = "Doctor", Rating = "Professional" },
            new OccupationRating { Occupation = "Author", Rating = "White Collar" },
            new OccupationRating { Occupation = "Farmer", Rating = "Heavy Manual" },
            new OccupationRating { Occupation = "Mechanic", Rating = "Heavy Manual" },
            new OccupationRating { Occupation = "Florist", Rating = "Heavy Manual" }});

            context.SaveChanges();
            _controller = new OccupationsController(context);
        }

        [Fact]
        public void Get_ShouldReturnOccupationsList()
        {
            var occupationsList = _controller.Get();
            //Verifying if Occupation count matches
            Assert.Equal(this.context.OccupationRatings.ToListAsync().Result.Count, occupationsList.Count);

            //Verifying if Occupations list match
            var occupations = this.context.OccupationRatings.ToListAsync().Result.Select(data => data.Occupation);
            Assert.Equal(occupations, occupationsList);
        }
    }
}
