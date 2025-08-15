using System;
using System.Collections.Generic;
using JOIEnergy.Enums;

namespace JOIEnergy.Services
{
    public class AccountService : IAccountService
    { 
        // IMPROVEMENT NEEDED: Make field readonly and use proper naming
        // WHAT: Add readonly modifier and use consistent naming convention
        // WHY: Prevents accidental modification, better encapsulation, consistent naming
        private Dictionary<string, string> _smartMeterToPricePlanAccounts;

        public AccountService(Dictionary<string, string> smartMeterToPricePlanAccounts) {
            // IMPROVEMENT NEEDED: Add null check and defensive programming
            // WHAT: Validate input parameter is not null
            // WHY: Prevents NullReferenceException, better error handling, defensive programming
            _smartMeterToPricePlanAccounts = smartMeterToPricePlanAccounts;
        }

        // IMPROVEMENT NEEDED: Improve error handling and return value consistency
        // WHAT: Consider throwing specific exception or returning Result<T> pattern
        // WHY: Better error handling, more explicit about failure cases, easier debugging
        public string GetPricePlanIdForSmartMeterId(string smartMeterId) {
            // IMPROVEMENT NEEDED: Add input validation
            // WHAT: Validate smartMeterId parameter is not null/empty
            // WHY: Prevents invalid requests, better error handling, security improvement
            
            if (!_smartMeterToPricePlanAccounts.ContainsKey(smartMeterId))
            {
                // IMPROVEMENT NEEDED: Consider returning null vs throwing exception
                // WHAT: Decide on consistent error handling strategy
                // WHY: More predictable API behavior, easier for clients to handle
                return null;
            }
            return _smartMeterToPricePlanAccounts[smartMeterId];
        }
    }
}
