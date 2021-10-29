using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PerfJournal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            RunProgram(builder);
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                return Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();

                    }).UseWindowsService();
            }
            else
            {
                return Host.CreateDefaultBuilder(args).
                       UseSystemd()
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();

                       }).UseSystemd();
            }
        }

        private static async void RunProgram(IHost host)
        {
            host.Run();
        }
    }
}
