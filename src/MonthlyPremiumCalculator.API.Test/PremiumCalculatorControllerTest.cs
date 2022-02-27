using Microsoft.EntityFrameworkCore;
using MonthlyPremiumCalculator.API.Controllers;
using MonthlyPremiumCalculator.API.DatabaseModels;
using MonthlyPremiumCalculator.API.Models;
using System.Collections.Generic;
using Xunit;

namespace MonthlyPremiumCalculator.API.Test
{
    public class PremiumCalculatorControllerTest
    {
        private readonly PremiumCalculatorController _controller;
        private readonly ApiContext context;

        public PremiumCalculatorControllerTest()
        {
            //Using EFCore Inmemory database for mocking DbContext
            var options = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase("PremiumCalculatorDatabase").Options;

            context = new ApiContext(options);

            //Checking if the values already exist as Constructor runs for each test case
            if (context.OccupationRatings.CountAsync().Result == 0)
            {
                context.OccupationRatings.AddRange(new List<OccupationRating>()
                                        { new OccupationRating { Occupation = "Cleaner", Rating = "Light Manual" },
                                        new OccupationRating { Occupation = "Doctor", Rating = "Professional" },
                                        new OccupationRating { Occupation = "Author", Rating = "White Collar" },
                                        new OccupationRating { Occupation = "Farmer", Rating = "Heavy Manual" }});
            }

            if (context.RatingFactors.CountAsync().Result ==0)
            {
                context.RatingFactors.AddRange(new List<RatingFactor>() { new RatingFactor { Rating="Professional", Factor=1.0M },
                                        new RatingFactor { Rating="White Collar", Factor=1.25M },
                                        new RatingFactor { Rating="Light Manual", Factor=1.50M },
                                        new RatingFactor { Rating="Heavy Manual", Factor=1.75M } });
            }            

            context.SaveChanges();
            _controller = new PremiumCalculatorController(context);
        }

        [Fact]
        public void Get_ShouldReturnMonthlyPremiumForLightManualRating()
        {
            var monthlyPremium = _controller.Post(new PremiumInputModel { 
                Age = 32,
                DateOfBirth = "20-11-1989",
                DeathSumInsured=1000,
                Name = "Test User",
                Occupation = "Cleaner"
            });
            //Verifying if monthly premium matches for Light Manual Rating
            Assert.Equal(576, monthlyPremium);
        }

        [Fact]
        public void Get_ShouldReturnMonthlyPremiumForHeavyManualRating()
        {
            var monthlyPremium = _controller.Post(new PremiumInputModel
            {
                Age = 32,
                DateOfBirth = "20-11-1989",
                DeathSumInsured = 1000,
                Name = "Test User1",
                Occupation = "Farmer"
            });
            //Verifying if monthly premium matches for Heavy Manual Rating
            Assert.Equal(672, monthlyPremium);
        }

        [Fact]
        public void Get_ShouldReturnMonthlyPremiumForWhiteCollarRating()
        {
            var monthlyPremium = _controller.Post(new PremiumInputModel
            {
                Age = 28,
                DateOfBirth = "20-11-1993",
                DeathSumInsured = 1000,
                Name = "Test User2",
                Occupation = "Author"
            });
            //Verifying if monthly premium matches for White Collar Rating
            Assert.Equal(420, monthlyPremium);
        }

        [Fact]
        public void Get_ShouldReturnMonthlyPremiumForProfessionalRating()
        {
            var monthlyPremium = _controller.Post(new PremiumInputModel
            {
                Age = 30,
                DateOfBirth = "20-11-1991",
                DeathSumInsured = 1000,
                Name = "Test User3",
                Occupation = "Doctor"
            });
            //Verifying if monthly premium matches for Professional Rating
            Assert.Equal(360, monthlyPremium);
        }
    }
}
