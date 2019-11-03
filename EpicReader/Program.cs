using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EpicReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:80;http://*:443");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
