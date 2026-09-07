using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.UI.Tests.Pages
{
    public class CheckoutPage
    {
        private readonly IPage _page;

        public CheckoutPage(IPage page)
        {
            _page = page;
        }

        public async Task CompleteCheckoutAsync()
        {
            // We will implement the exact billing,
            // shipping and payment steps after
            // inspecting your nopCommerce checkout DOM.
        }
    }
}
