using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SimpleWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                string path = context.Request.Path;

                if (path == "/api/getdata" && context.Request.Method == "GET")
                {
                    await context.Response.WriteAsync("GET request received successfully!");
                }
                else if (path == "/api/postdata" && context.Request.Method == "POST")
                {
                    string data = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();
                    await context.Response.WriteAsync($"POST request received successfully! Data: {data}");
                }
                else
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync("Not Found");
                }
            });
        }
    }
}
