using System;
using System.Collections.Generic;
using JOIEnergy.Services;
using JOIEnergy.Domain;
using Xunit;

namespace JOIEnergy.Tests
{
    // IMPROVEMENT NEEDED: Improve test class naming and organization
    // WHAT: Use more descriptive class name, add test categories, organize tests by functionality
    // WHY: Better test organization, easier to find specific tests, follows testing best practices
    public class MeterReadingServiceTest
    {
        // IMPROVEMENT NEEDED: Move magic strings to constants
        // WHAT: Extract test data to constants or test data builders
        // WHY: Easier to maintain, follows DRY principle, reduces typos
        private static string SMART_METER_ID = "smart-meter-id";

        // IMPROVEMENT NEEDED: Use proper naming convention and readonly modifier
        // WHAT: Use PascalCase naming and add readonly modifier
        // WHY: Follows C# naming conventions, prevents accidental modification
        private MeterReadingService meterReadingService;

        // IMPROVEMENT NEEDED: Improve test setup and use proper test patterns
        // WHAT: Use constructor for setup, consider using IClassFixture or ICollectionFixture
        // WHY: Better test isolation, follows xUnit best practices, more maintainable tests
        public MeterReadingServiceTest()
        {
            meterReadingService = new MeterReadingService(new Dictionary<string, List<ElectricityReading>>());

            // IMPROVEMENT NEEDED: Move test data setup to separate method
            // WHAT: Extract test data creation to helper methods or test data builders
            // WHY: Better test readability, reusable test data, easier maintenance
            meterReadingService.StoreReadings(SMART_METER_ID, new List<ElectricityReading>() {
                new ElectricityReading() { Time = DateTime.Now.AddMinutes(-30), Reading = 35m },
                new ElectricityReading() { Time = DateTime.Now.AddMinutes(-15), Reading = 30m }
            });
        }

        // IMPROVEMENT NEEDED: Improve test method naming and add more test cases
        // WHAT: Use more descriptive test names, add edge cases, test error conditions
        // WHY: Better test coverage, clearer test intent, easier debugging
        [Fact]
        public void GivenMeterIdThatDoesNotExistShouldReturnNull() {
            // IMPROVEMENT NEEDED: Add more comprehensive assertions
            // WHAT: Test that returned list is empty, not null, and has correct count
            // WHY: Better test validation, ensures expected behavior, prevents regressions
            Assert.Empty(meterReadingService.GetReadings("unknown-id"));
        }

        // IMPROVEMENT NEEDED: Improve test method naming and add more test scenarios
        // WHAT: Use more descriptive test names, test different scenarios, add edge cases
        // WHY: Better test coverage, clearer test intent, easier debugging
        [Fact]
        public void GivenMeterReadingThatExistsShouldReturnMeterReadings()
        {
            // IMPROVEMENT NEEDED: Use test data builders and improve test data
            // WHAT: Create test data using builders, use more realistic test scenarios
            // WHY: Better test data quality, more maintainable tests, realistic testing
            meterReadingService.StoreReadings(SMART_METER_ID, new List<ElectricityReading>() {
                new ElectricityReading() { Time = DateTime.Now, Reading = 25m }
            });

            var electricityReadings = meterReadingService.GetReadings(SMART_METER_ID);

            // IMPROVEMENT NEEDED: Add more comprehensive assertions
            // WHAT: Test list count, content, order, and data integrity
            // WHY: Better test validation, ensures expected behavior, prevents regressions
            Assert.Equal(3, electricityReadings.Count);
        }
        
        // IMPROVEMENT NEEDED: Add more test scenarios
        // WHAT: Test null inputs, empty lists, edge cases, error conditions
        // WHY: Better test coverage, ensures robustness, prevents runtime errors
        
        // IMPROVEMENT NEEDED: Add integration tests
        // WHAT: Test with real data scenarios, test service interactions
        // WHY: Ensures components work together, catches integration issues
    }
}
