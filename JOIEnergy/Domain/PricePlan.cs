using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Enums;

namespace JOIEnergy.Domain
{
    // IMPROVEMENT NEEDED: Add data validation and improve domain modeling
    // WHAT: Add validation attributes, consider making properties init-only, add business rules
    // WHY: Ensures data integrity, prevents invalid data, better domain modeling
    public class PricePlan
    {
        // IMPROVEMENT NEEDED: Add validation attributes and business rules
        // WHAT: Add [Required] attribute, validate plan name format
        // WHY: Prevents invalid data, better data quality, automatic validation
        public string PlanName { get; set; }
        
        // IMPROVEMENT NEEDED: Add validation and improve enum usage
        // WHAT: Add [Required] attribute, consider using enum validation
        // WHY: Prevents invalid data, better data quality, enum validation
        public Supplier EnergySupplier { get; set; }
        
        // IMPROVEMENT NEEDED: Add validation and consider decimal precision
        // WHAT: Add [Range] attribute to ensure positive values, consider decimal(18,4)
        // WHY: Prevents negative rates, better data accuracy, business rule enforcement
        public decimal UnitRate { get; set; }
        
        // IMPROVEMENT NEEDED: Improve collection handling and validation
        // WHAT: Add [Required] attribute, consider using IReadOnlyList, validate collection contents
        // WHY: Prevents invalid data, better encapsulation, immutable collections
        public IList<PeakTimeMultiplier> PeakTimeMultiplier { get; set;}

        // IMPROVEMENT NEEDED: Improve method implementation and error handling
        // WHAT: Add null checks, improve LINQ usage, add validation
        // WHY: Prevents NullReferenceException, better performance, safer code
        public decimal GetPrice(DateTime datetime) {
            // IMPROVEMENT NEEDED: Add null check for PeakTimeMultiplier
            // WHAT: Check if collection is null before using LINQ methods
            // WHY: Prevents NullReferenceException, better error handling
            
            var multiplier = PeakTimeMultiplier.FirstOrDefault(m => m.DayOfWeek == datetime.DayOfWeek);

            if (multiplier?.Multiplier != null) {
                return multiplier.Multiplier * UnitRate;
            } else {
                return UnitRate;
            }
        }
        
        // IMPROVEMENT NEEDED: Add constructor with validation
        // WHAT: Add constructor that validates input parameters
        // WHY: Ensures valid object creation, better encapsulation, domain invariants
        
        // IMPROVEMENT NEEDED: Add business logic methods
        // WHAT: Add methods like IsPeakTime(), GetEffectiveRate(), ValidatePlan()
        // WHY: Encapsulates business logic, better domain modeling, reusable functionality
    }

    // IMPROVEMENT NEEDED: Add data validation and improve domain modeling
    // WHAT: Add validation attributes, consider making properties init-only, add business rules
    // WHY: Ensures data integrity, prevents invalid data, better domain modeling
    public class PeakTimeMultiplier
    {
        // IMPROVEMENT NEEDED: Add validation attributes and business rules
        // WHAT: Add [Required] attribute, validate day of week range
        // WHY: Prevents invalid data, better data quality, automatic validation
        public DayOfWeek DayOfWeek { get; set; }
        
        // IMPROVEMENT NEEDED: Add validation and consider decimal precision
        // WHAT: Add [Range] attribute to ensure positive values, consider decimal(18,4)
        // WHY: Prevents negative multipliers, better data accuracy, business rule enforcement
        public decimal Multiplier { get; set; }
        
        // IMPROVEMENT NEEDED: Add constructor with validation
        // WHAT: Add constructor that validates input parameters
        // WHY: Ensures valid object creation, better encapsulation, domain invariants
    }
}
