﻿using Azure.Identity;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureServices((builder, services) =>
            {
                services.AddLogging();
                services.AddHttpClient();
                services.AddMemoryCache();

                services.AddAutoMapper(typeof(Program));
            })
            .ConfigureFunctionsWorkerDefaults(workerApplication =>
             {
                 workerApplication.UseNewtonsoftJson();
             })
            .ConfigureAppConfiguration((ctx, configuration) =>
            {
                var settings = configuration.Build();
            })
            .Build();

        await host.RunAsync();
    }
}