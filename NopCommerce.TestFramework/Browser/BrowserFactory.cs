using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NopCommerce.TestFramework.Configuration;

namespace NopCommerce.TestFramework.Browser
{
    public class BrowserFactory
    {
        public async Task<IBrowser> CreateAsync(
            IPlaywright playwright)
        {
            var settings = ConfigurationManager.Settings.Browser;

            return await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = settings.Headless
                });
        }
    }
}
