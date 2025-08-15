using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Domain;
using JOIEnergy.Generator;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace JOIEnergy
{
    public class Startup
    {
        // IMPROVEMENT NEEDED: Move magic strings to configuration or constants file
        // WHAT: Extract these hardcoded strings to appsettings.json or a separate constants class
        // WHY: Makes the application configurable, easier to maintain, follows DRY principle
        private const string MOST_EVIL_PRICE_PLAN_ID = "price-plan-0";
        private const string RENEWABLES_PRICE_PLAN_ID = "price-plan-1";
        private const string STANDARD_PRICE_PLAN_ID = "price-plan-2";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // IMPROVEMENT NEEDED: Move data generation out of Startup class
            // WHAT: Create a separate data seeding service or move to Program.cs
            // WHY: Startup should only configure services, not generate data - violates single responsibility principle
            var readings =
                GenerateMeterElectricityReadings();

            // IMPROVEMENT NEEDED: Move hardcoded data to configuration or database
            // WHAT: Extract price plans to appsettings.json or create a data seeding service
            // WHY: Makes the application configurable, easier to test, follows configuration best practices
            var pricePlans = new List<PricePlan> {
                new PricePlan{
                    PlanName = MOST_EVIL_PRICE_PLAN_ID,
                    EnergySupplier = Enums.Supplier.DrEvilsDarkEnergy,
                    UnitRate = 10m,
                    PeakTimeMultiplier = new List<PeakTimeMultiplier>()
                },
                new PricePlan{
                    PlanName = RENEWABLES_PRICE_PLAN_ID,
                    EnergySupplier = Enums.Supplier.TheGreenEco,
                    UnitRate = 2m,
                    PeakTimeMultiplier = new List<PeakTimeMultiplier>()
                },
                new PricePlan{
                    PlanName = STANDARD_PRICE_PLAN_ID,
                    EnergySupplier = Enums.Supplier.PowerForEveryone,
                    UnitRate = 1m,
                    PeakTimeMultiplier = new List<PeakTimeMultiplier>()
                }
            };

            // IMPROVEMENT NEEDED: Update to modern .NET 8 approach
            // WHAT: Replace AddMvc with AddControllers and AddEndpointsApiExplorer
            // WHY: AddMvc is deprecated, new approach is more performant and follows current standards
            services.AddMvc(options => options.EnableEndpointRouting = false);
            
            // IMPROVEMENT NEEDED: Review service lifetimes
            // WHAT: Consider if services should be Transient, Scoped, or Singleton based on usage patterns
            // WHY: Incorrect lifetimes can cause performance issues, memory leaks, or unexpected behavior
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IMeterReadingService, MeterReadingService>();
            services.AddTransient<IPricePlanService, PricePlanService>();
            
            // IMPROVEMENT NEEDED: Use proper singleton registration syntax
            // WHAT: Replace lambda-based registration with direct object registration
            // WHY: More explicit, easier to read, follows DI best practices
            services.AddSingleton((IServiceProvider arg) => readings);
            services.AddSingleton((IServiceProvider arg) => pricePlans);
            services.AddSingleton((IServiceProvider arg) => SmartMeterToPricePlanAccounts);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // IMPROVEMENT NEEDED: Update to modern .NET 8 routing
            // WHAT: Replace UseMvc with UseRouting and MapControllers
            // WHY: UseMvc is deprecated, new approach provides better performance and flexibility
            app.UseMvc();
        }

        // IMPROVEMENT NEEDED: Move data generation logic to separate service
        // WHAT: Create a DataSeedingService or move to Program.cs
        // WHY: Startup class should focus on configuration, not business logic
        private Dictionary<string, List<ElectricityReading>> GenerateMeterElectricityReadings() {
            var readings = new Dictionary<string, List<ElectricityReading>>();
            var generator = new ElectricityReadingGenerator();
            var smartMeterIds = SmartMeterToPricePlanAccounts.Select(mtpp => mtpp.Key);

            foreach (var smartMeterId in smartMeterIds)
            {
                readings.Add(smartMeterId, generator.Generate(20));
            }
            return readings;
        }

        // IMPROVEMENT NEEDED: Move hardcoded mapping to configuration
        // WHAT: Extract to appsettings.json or create a configuration service
        // WHY: Makes the application configurable, easier to maintain, follows configuration best practices
        public Dictionary<string, string> SmartMeterToPricePlanAccounts
        {
            get
            {
                Dictionary<string, string> smartMeterToPricePlanAccounts = new Dictionary<string, string>();
                smartMeterToPricePlanAccounts.Add("smart-meter-0", MOST_EVIL_PRICE_PLAN_ID);
                smartMeterToPricePlanAccounts.Add("smart-meter-1", RENEWABLES_PRICE_PLAN_ID);
                smartMeterToPricePlanAccounts.Add("smart-meter-2", MOST_EVIL_PRICE_PLAN_ID);
                smartMeterToPricePlanAccounts.Add("smart-meter-3", STANDARD_PRICE_PLAN_ID);
                smartMeterToPricePlanAccounts.Add("smart-meter-4", RENEWABLES_PRICE_PLAN_ID);
                return smartMeterToPricePlanAccounts;
            }
        }
    }
}
