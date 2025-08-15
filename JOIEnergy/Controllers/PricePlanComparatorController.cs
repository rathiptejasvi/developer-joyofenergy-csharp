using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Enums;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    // IMPROVEMENT NEEDED: Add API versioning and proper route attributes
    // WHAT: Add [ApiController] attribute and consider API versioning
    // WHY: Better API documentation, automatic model validation, follows REST API best practices
    [Route("price-plans")]
    public class PricePlanComparatorController : Controller
    {
        // IMPROVEMENT NEEDED: Move magic strings to constants or configuration
        // WHAT: Extract these strings to a constants file or configuration
        // WHY: Easier to maintain, follows DRY principle, reduces typos
        public const string PRICE_PLAN_ID_KEY = "pricePlanId";
        public const string PRICE_PLAN_COMPARISONS_KEY = "pricePlanComparisons";
        
        private readonly IPricePlanService _pricePlanService;
        private readonly IAccountService _accountService;

        public PricePlanComparatorController(IPricePlanService pricePlanService, IAccountService accountService)
        {
            // IMPROVEMENT NEEDED: Use proper field naming convention
            // WHAT: Remove 'this.' prefix, use consistent naming
            // WHY: Follows C# naming conventions, cleaner code, better readability
            this._pricePlanService = pricePlanService;
            this._accountService = accountService;
        }

        // IMPROVEMENT NEEDED: Improve method naming and return type
        // WHAT: Use more descriptive method name, consider returning ActionResult<T>
        // WHY: Better readability, follows C# naming conventions, more type-safe
        [HttpGet("compare-all/{smartMeterId}")]
        public ObjectResult CalculatedCostForEachPricePlan(string smartMeterId)
        {
            // IMPROVEMENT NEEDED: Add input validation
            // WHAT: Validate smartMeterId parameter is not null/empty
            // WHY: Prevents invalid requests, better error handling, security improvement
            
            string pricePlanId = _accountService.GetPricePlanIdForSmartMeterId(smartMeterId);
            Dictionary<string, decimal> costPerPricePlan = _pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);
            
            // IMPROVEMENT NEEDED: Improve error handling logic
            // WHAT: Check if pricePlanId is null before checking costPerPricePlan
            // WHY: More accurate error detection, better user experience
            if (!costPerPricePlan.Any())
            {
                // IMPROVEMENT NEEDED: Use string interpolation instead of string.Format
                // WHAT: Replace string.Format with $"" syntax
                // WHY: More readable, better performance, modern C# syntax
                return new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
            }

            // IMPROVEMENT NEEDED: Improve response structure and type safety
            // WHAT: Use strongly-typed response model instead of Dictionary<string, object>
            // WHY: Better type safety, easier to maintain, clearer API contract
            return new ObjectResult(new Dictionary<string, object>() {
                {PRICE_PLAN_ID_KEY, pricePlanId},
                {PRICE_PLAN_COMPARISONS_KEY, costPerPricePlan},
            });
        }

        // IMPROVEMENT NEEDED: Improve method naming and error handling
        // WHAT: Use more descriptive method name, improve error handling consistency
        // WHY: Better readability, consistent error handling, follows naming conventions
        [HttpGet("recommend/{smartMeterId}")]
        public ObjectResult RecommendCheapestPricePlans(string smartMeterId, int? limit = null) {
            // IMPROVEMENT NEEDED: Add input validation
            // WHAT: Validate smartMeterId parameter and limit value (should be > 0)
            // WHY: Prevents invalid requests, better error handling, security improvement
            
            var consumptionForPricePlans = _pricePlanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);

            // IMPROVEMENT NEEDED: Improve error handling consistency
            // WHAT: Use same error handling pattern as other method
            // WHY: Consistent API behavior, easier for clients to handle, better user experience
            if (!consumptionForPricePlans.Any()) {
                return new NotFoundObjectResult(string.Format("Smart Meter ID ({0}) not found", smartMeterId));
            }

            // IMPROVEMENT NEEDED: Improve variable naming
            // WHAT: Use more descriptive variable name (e.g., 'orderedRecommendations')
            // WHY: Better readability, clearer intent, follows naming conventions
            var recommendations = consumptionForPricePlans.OrderBy(pricePlanComparison => pricePlanComparison.Value);

            // IMPROVEMENT NEEDED: Improve limit validation logic
            // WHAT: Add validation that limit is positive, improve the logic flow
            // WHY: Prevents invalid limit values, clearer logic, better error handling
            if (limit.HasValue && limit.Value < recommendations.Count())
            {
                return new ObjectResult(recommendations.Take(limit.Value));
            }

            return new ObjectResult(recommendations);
        }
    }
}
