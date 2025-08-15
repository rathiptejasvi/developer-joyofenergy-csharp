using System;
using System.Collections.Generic;

namespace JOIEnergy.Domain
{
    // IMPROVEMENT NEEDED: Add data validation and improve domain modeling
    // WHAT: Add validation attributes, consider making properties init-only, add business rules
    // WHY: Ensures data integrity, prevents invalid data, better domain modeling
    public class MeterReadings
    {
        // IMPROVEMENT NEEDED: Add validation attributes and business rules
        // WHAT: Add [Required] attribute, validate smart meter ID format
        // WHY: Prevents invalid data, better data quality, automatic validation
        public string SmartMeterId { get; set; }
        
        // IMPROVEMENT NEEDED: Add validation and improve collection handling
        // WHAT: Add [Required] attribute, validate collection is not empty, consider using IReadOnlyList
        // WHY: Prevents invalid data, better encapsulation, immutable collections
        public List<ElectricityReading> ElectricityReadings { get; set; }
        
        // IMPROVEMENT NEEDED: Add constructor with validation
        // WHAT: Add constructor that validates input parameters
        // WHY: Ensures valid object creation, better encapsulation, domain invariants
        
        // IMPROVEMENT NEEDED: Add business logic methods
        // WHAT: Add methods like GetTotalConsumption(), GetAverageReading(), GetTimeSpan()
        // WHY: Encapsulates business logic, better domain modeling, reusable functionality
        
        // IMPROVEMENT NEEDED: Override ToString for better debugging
        // WHAT: Add meaningful ToString() implementation
        // WHY: Better debugging experience, logging, user interface display
    }
}
