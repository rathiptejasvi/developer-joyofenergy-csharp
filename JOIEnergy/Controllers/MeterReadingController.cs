using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JOIEnergy.Domain;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    // IMPROVEMENT NEEDED: Add API versioning and proper route attributes
    // WHAT: Add [ApiController] attribute and consider API versioning
    // WHY: Better API documentation, automatic model validation, follows REST API best practices
    [Route("readings")]
    public class MeterReadingController : Controller
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }
        
        // IMPROVEMENT NEEDED: Fix HTTP status code and response format
        // WHAT: Return proper HTTP status codes (201 Created for POST) and consistent response format
        // WHY: Follows HTTP standards, better API design, easier for clients to handle responses
        [HttpPost ("store")]
        public ObjectResult Post([FromBody]MeterReadings meterReadings)
        {
            // IMPROVEMENT NEEDED: Improve error handling and validation
            // WHAT: Add proper model validation, return specific error messages, use ProblemDetails
            // WHY: Better user experience, easier debugging, follows REST API error handling standards
            if (!IsMeterReadingsValid(meterReadings)) {
                // IMPROVEMENT NEEDED: Fix incorrect error message
                // WHAT: Change "Internal Server Error" to "Bad Request" and provide specific validation errors
                // WHY: "Internal Server Error" is misleading for validation failures, should be 400 not 500
                return new BadRequestObjectResult("Internal Server Error");
            }
            
            // IMPROVEMENT NEEDED: Add error handling for service operations
            // WHAT: Wrap in try-catch, handle potential exceptions from service layer
            // WHY: Prevents unhandled exceptions from reaching client, better error reporting
            _meterReadingService.StoreReadings(meterReadings.SmartMeterId,meterReadings.ElectricityReadings);
            
            // IMPROVEMENT NEEDED: Return proper response format
            // WHAT: Return 201 Created with location header or meaningful response object
            // WHY: Follows HTTP standards for resource creation, provides better client experience
            return new OkObjectResult("{}");
        }

        // IMPROVEMENT NEEDED: Improve validation logic and error messages
        // WHAT: Add more comprehensive validation, return specific error details, use FluentValidation
        // WHY: Better user experience, easier debugging, prevents invalid data from being stored
        private bool IsMeterReadingsValid(MeterReadings meterReadings)
        {
            // IMPROVEMENT NEEDED: Add null check for meterReadings parameter
            // WHAT: Check if meterReadings is null before accessing its properties
            // WHY: Prevents NullReferenceException, better defensive programming
            string smartMeterId = meterReadings.SmartMeterId;
            List<ElectricityReading> electricityReadings = meterReadings.ElectricityReadings;
            
            // IMPROVEMENT NEEDED: Improve validation logic
            // WHAT: Add more specific validation (e.g., valid smart meter ID format, reading values > 0)
            // WHY: Prevents invalid data, better data quality, easier debugging
            return smartMeterId != null && smartMeterId.Any()
                    && electricityReadings != null && electricityReadings.Any();
        }

        // IMPROVEMENT NEEDED: Add proper error handling and validation
        // WHAT: Validate smartMeterId parameter, handle service exceptions, return appropriate HTTP status codes
        // WHY: Better error handling, prevents crashes, follows REST API best practices
        [HttpGet("read/{smartMeterId}")]
        public ObjectResult GetReading(string smartMeterId) {
            // IMPROVEMENT NEEDED: Add input validation
            // WHAT: Validate smartMeterId is not null/empty, check format
            // WHY: Prevents invalid requests, better error handling, security improvement
            
            // IMPROVEMENT NEEDED: Handle empty results properly
            // WHAT: Return 404 Not Found when no readings exist for the meter
            // WHY: Follows HTTP standards, better API design, clearer client experience
            return new OkObjectResult(_meterReadingService.GetReadings(smartMeterId));
        }
    }
}
