using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonthlyPremiumCalculator.API.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremiumCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationsController : ControllerBase
    {
        private readonly ApiContext _context;

        public OccupationsController(ApiContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public List<string> Get()
        {
            List<OccupationRating> occupationRatings = _context.OccupationRatings.ToList();

            return occupationRatings.Select(occupationRating => occupationRating.Occupation).ToList();
        }
    }
}
