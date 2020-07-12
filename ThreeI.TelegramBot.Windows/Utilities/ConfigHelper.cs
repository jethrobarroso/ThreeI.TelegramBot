﻿using Microsoft.Extensions.Configuration;
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

            foreach (var block in blocks)
                result += block + Environment.NewLine;

            return result;
        }

        private static string[] GetBlocks(IConfiguration config, string key)
        {
            return config.GetSection(key).Get<string[]>();
        }
    }
}
