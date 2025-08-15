using System;
namespace JOIEnergy.Domain
{
    // IMPROVEMENT NEEDED: Add data validation and immutability
    // WHAT: Add validation attributes, consider making properties init-only, add business rules
    // WHY: Ensures data integrity, prevents invalid data, better domain modeling
    public class ElectricityReading
    {
        // IMPROVEMENT NEEDED: Add validation attributes and business rules
        // WHAT: Add [Required], [Range] attributes, validate that reading is positive
        // WHY: Prevents invalid data, better data quality, automatic validation
        public DateTime Time { get; set; }
        
        // IMPROVEMENT NEEDED: Add validation and consider decimal precision
        // WHAT: Add validation that reading > 0, consider using decimal(18,4) for precision
        // WHY: Prevents negative readings, better data accuracy, business rule enforcement
        public Decimal Reading { get; set; }
        
        // IMPROVEMENT NEEDED: Add constructor with validation
        // WHAT: Add constructor that validates input parameters
        // WHY: Ensures valid object creation, better encapsulation, domain invariants
        
        // IMPROVEMENT NEEDED: Override ToString for better debugging
        // WHAT: Add meaningful ToString() implementation
        // WHY: Better debugging experience, logging, user interface display
    }
}
