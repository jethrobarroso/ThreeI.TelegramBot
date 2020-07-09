using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Windows.Utilities;

namespace ThreeI.TelegramBot.Test.Windows
{
    public class ConfigHelperTest
    {
        [Test]
        public void ConfigHelper_Collection_ValidConfigArray()
        {
            IConfiguration config = TestConfigHelper.InitConfiguration();
            var expected = new List<string>() { "1401", "1404" };
            var actual = ConfigHelper.GetBlockCollection(config, "BlockNumbers");

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
