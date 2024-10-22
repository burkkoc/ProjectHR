using Microsoft.Extensions.Configuration;
using System.IO;

namespace IKProject.Infrastructure.Configuration
{
    public static class Configuration
    {
        public static IConfigurationRoot GetConfiguration()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../IKProject.API"));
            builder.AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}
