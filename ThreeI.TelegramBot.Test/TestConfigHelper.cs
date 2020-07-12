using Microsoft.Extensions.Configuration;

namespace ThreeI.TelegramBot.Test
{
    public static class TestConfigHelper
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}
