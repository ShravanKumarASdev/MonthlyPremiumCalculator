using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthlyPremiumCalculator.API.Models
{
    public class PremiumInputModel
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public double DeathSumInsured { get; set; }
    }
}
