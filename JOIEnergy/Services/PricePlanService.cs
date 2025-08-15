using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class PricePlanService : IPricePlanService
    {
        // IMPROVEMENT NEEDED: Remove unused debug interface
        // WHAT: Delete this unused interface declaration
        // WHY: Dead code, reduces readability, maintenance overhead
        public interface Debug { void Log(string s); };

        private readonly List<PricePlan> _pricePlans;
        // IMPROVEMENT NEEDED: Make field readonly and use proper naming
        // WHAT: Add readonly modifier and use consistent naming convention
        // WHY: Prevents accidental modification, better encapsulation, consistent naming
        private IMeterReadingService _meterReadingService;

        public PricePlanService(List<PricePlan> pricePlan, IMeterReadingService meterReadingService)
        {
            // IMPROVEMENT NEEDED: Add null checks and defensive programming
            // WHAT: Validate both parameters are not null
            // WHY: Prevents NullReferenceException, better error handling, defensive programming
            _pricePlans = pricePlan;
            _meterReadingService = meterReadingService;
        }

        // IMPROVEMENT NEEDED: Improve method naming and algorithm efficiency
        // WHAT: Use PascalCase naming, consider using LINQ Average() method
        // WHY: Follows C# naming conventions, better performance, more readable
        private decimal calculateAverageReading(List<ElectricityReading> electricityReadings)
        {
            // IMPROVEMENT NEEDED: Use LINQ Average() method for better performance
            // WHAT: Replace Select().Aggregate() with Average() method
            // WHY: Better performance, more readable, built-in optimization
            var newSummedReadings = electricityReadings.Select(readings => readings.Reading).Aggregate((reading, accumulator) => reading + accumulator);

            return newSummedReadings / electricityReadings.Count();
        }

        // IMPROVEMENT NEEDED: Improve method naming and error handling
        // WHAT: Use PascalCase naming, handle empty list case
        // WHY: Follows C# naming conventions, prevents division by zero, better error handling
        private decimal calculateTimeElapsed(List<ElectricityReading> electricityReadings)
        {
            // IMPROVEMENT NEEDED: Add validation for empty list
            // WHAT: Check if list is empty before calling Min/Max
            // WHY: Prevents InvalidOperationException, better error handling
            
            var first = electricityReadings.Min(reading => reading.Time);
            var last = electricityReadings.Max(reading => reading.Time);

            return (decimal)(last - first).TotalHours;
        }
        
        // IMPROVEMENT NEEDED: Improve method naming and calculation logic
        // WHAT: Use PascalCase naming, review the calculation formula
        // WHY: Follows C# naming conventions, the current formula seems incorrect for cost calculation
        private decimal calculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan)
        {
            var average = calculateAverageReading(electricityReadings);
            var timeElapsed = calculateTimeElapsed(electricityReadings);
            
            // IMPROVEMENT NEEDED: Review cost calculation formula
            // WHAT: The current formula (average/timeElapsed * unitRate) seems incorrect
            // WHY: Should probably be (average * timeElapsed * unitRate) for proper cost calculation
            
            var averagedCost = average/timeElapsed;
            return Math.Round(averagedCost * pricePlan.UnitRate, 3);
        }

        // IMPROVEMENT NEEDED: Improve method naming and error handling
        // WHAT: Use PascalCase naming, add better error handling
        // WHY: Follows C# naming conventions, better error reporting, user experience
        public Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId)
        {
            // IMPROVEMENT NEEDED: Add input validation
            // WHAT: Validate smartMeterId parameter is not null/empty
            // WHY: Prevents invalid requests, better error handling, security improvement
            
            List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId);

            // IMPROVEMENT NEEDED: Improve empty result handling
            // WHAT: Consider returning null or throwing specific exception for better error handling
            // WHY: More accurate error reporting, better client experience
            if (!electricityReadings.Any())
            {
                return new Dictionary<string, decimal>();
            }
            
            // IMPROVEMENT NEEDED: Consider using the unused GetPrice method from PricePlan
            // WHAT: Use pricePlan.GetPrice() method instead of just UnitRate
            // WHY: Takes advantage of existing peak time multiplier logic, more accurate pricing
            return _pricePlans.ToDictionary(plan => plan.PlanName, plan => calculateCost(electricityReadings, plan));
        }
    }
}
