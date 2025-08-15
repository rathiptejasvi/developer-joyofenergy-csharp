using System;
using System.Collections.Generic;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        // IMPROVEMENT NEEDED: Make property private and add proper encapsulation
        // WHAT: Change to private field, add public method to access data if needed
        // WHY: Better encapsulation, prevents external modification, follows OOP principles
        public Dictionary<string, List<ElectricityReading>> MeterAssociatedReadings { get; set; }
        
        public MeterReadingService(Dictionary<string, List<ElectricityReading>> meterAssociatedReadings)
        {
            // IMPROVEMENT NEEDED: Add null check and defensive programming
            // WHAT: Validate input parameter is not null
            // WHY: Prevents NullReferenceException, better error handling, defensive programming
            MeterAssociatedReadings = meterAssociatedReadings;
        }

        // IMPROVEMENT NEEDED: Improve error handling and return value consistency
        // WHAT: Consider returning null for non-existent meters or throw specific exception
        // WHY: More consistent API behavior, better error handling, clearer intent
        public List<ElectricityReading> GetReadings(string smartMeterId) {
            // IMPROVEMENT NEEDED: Add input validation
            // WHAT: Validate smartMeterId parameter is not null/empty
            // WHY: Prevents invalid requests, better error handling, security improvement
            
            if (MeterAssociatedReadings.ContainsKey(smartMeterId)) {
                return MeterAssociatedReadings[smartMeterId];
            }
            // IMPROVEMENT NEEDED: Consider returning null instead of empty list
            // WHAT: Return null for non-existent meters or throw NotFoundException
            // WHY: Distinguishes between "no readings" and "meter doesn't exist", better error handling
            return new List<ElectricityReading>();
        }

        // IMPROVEMENT NEEDED: Improve error handling and data validation
        // WHAT: Add validation for electricityReadings, handle potential exceptions
        // WHY: Better data quality, prevents invalid data storage, easier debugging
        public void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings) {
            // IMPROVEMENT NEEDED: Add input validation
            // WHAT: Validate both parameters are not null/empty
            // WHY: Prevents invalid data storage, better error handling, data integrity
            
            if (!MeterAssociatedReadings.ContainsKey(smartMeterId)) {
                MeterAssociatedReadings.Add(smartMeterId, new List<ElectricityReading>());
            }

            // IMPROVEMENT NEEDED: Improve data addition logic
            // WHAT: Use AddRange instead of ForEach, consider validation before adding
            // WHY: Better performance, more readable, easier to maintain
            electricityReadings.ForEach(electricityReading => MeterAssociatedReadings[smartMeterId].Add(electricityReading));
        }
    }
}
