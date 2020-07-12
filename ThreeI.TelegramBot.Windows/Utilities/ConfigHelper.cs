using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ThreeI.TelegramBot.Windows.Utilities
{
    public static class ConfigHelper
    {
        public static IEnumerable<string> GetBlockCollection(IConfiguration config, string key)
        {
            var blocks = GetBlocks(config, key);
            return blocks;
        }

        public static string GetBlockListInText(IConfiguration config, string key)
        {
            var blocks = GetBlocks(config, key);
            string result = string.Empty;

            for (int i = 0; i < blocks.Length; i++)
            {
                string block = blocks[i];
                result += (i < blocks.Length-1) ? $"{block}\n" : $"{block}";
            }

            return result;
        }

        private static string[] GetBlocks(IConfiguration config, string key)
        {
            return config.GetSection(key).Get<string[]>();
        }
    }
}
