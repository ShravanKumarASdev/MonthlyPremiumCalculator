using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonthlyPremiumCalculator.API.DatabaseModels;
using MonthlyPremiumCalculator.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremiumCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiumCalculatorController : ControllerBase
    {
        private readonly ApiContext _context;

        public PremiumCalculatorController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public decimal Post([FromBody]PremiumInputModel premiumInputModel)
        {
            List<OccupationRating> occupationRatings = _context.OccupationRatings.ToList();
            List<RatingFactor> ratingFactors = _context.RatingFactors.ToList();

            var selectedRating = occupationRatings.FirstOrDefault
                (occupationRating => occupationRating.Occupation == premiumInputModel.Occupation).Rating;

            var selectedRatingFactor = ratingFactors.FirstOrDefault
                (ratingFactor => ratingFactor.Rating == selectedRating).Factor;

            return ((decimal)premiumInputModel.DeathSumInsured * (decimal)premiumInputModel.Age * selectedRatingFactor) / 1000 * 12;
        }
    }
}
