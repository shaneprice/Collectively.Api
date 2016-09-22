﻿using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Coolector.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://*:5000")
                .Build();

            host.Run();
        }
    }
}