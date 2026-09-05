using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.TestFramework.Configuration
{
    public class TestSettings
    {
        public string Environment { get; set; } = "Local";

        public ApplicationSettings Application { get; set; } = new();

        public ApiSettings API { get; set; } = new();

        public DatabaseSettings Database { get; set; } = new();

        public BrowserSettings Browser { get; set; } = new();
    }
}
