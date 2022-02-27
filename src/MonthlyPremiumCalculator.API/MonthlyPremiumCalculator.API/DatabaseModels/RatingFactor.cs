using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremiumCalculator.API.DatabaseModels
{
    public class RatingFactor
    {
        public decimal Factor { get; set; }
        [Key]
        public string Rating { get; set; }
    }
}
