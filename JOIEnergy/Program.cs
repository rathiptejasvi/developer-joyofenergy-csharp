using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JOIEnergy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        // IMPROVEMENT NEEDED: Modernize to .NET 8 minimal hosting model
        // WHAT: Replace WebHost.CreateDefaultBuilder with WebApplication.CreateBuilder
        // WHY: Better performance, cleaner code, follows current .NET best practices, deprecated approach
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
