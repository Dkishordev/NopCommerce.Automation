using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.TestFramework.Configuration
{
    public class BrowserSettings
    {
        public string Browser { get; set; } = "chromium";

        public bool Headless { get; set; } = true;
    }
}
