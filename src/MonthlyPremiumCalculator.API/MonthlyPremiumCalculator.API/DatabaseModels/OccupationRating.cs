using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremiumCalculator.API.DatabaseModels
{
    public class OccupationRating
    {
        [Key]
        public string Occupation { get; set; }
        public string Rating { get; set; }
    }
}
