using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elephantshine.Models;
using elephantshine.Models.Enum;
using elephantshine.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace elephantshine.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly Settings settings;
        private readonly List<Portfolio> portfolios;
        private readonly List<EnumCategory> enableCategorys;

        public PortfolioController(
            IOptions<List<Portfolio>> portfolios, 
            IOptions<List<EnumCategory>> enableCategorys,
            IOptions<Settings> settings)
        {
            this.portfolios = portfolios.Value;
            this.enableCategorys = enableCategorys.Value;
            this.settings = settings.Value;
        }

        public IActionResult Index()
        {
            var viewModel = new PortfolioViewModel(portfolios, enableCategorys, settings);
            return View(viewModel);
        }

        [Route("/Portfolio/{name}")]
        public IActionResult Detail(string name)
        {
            var portfolio = portfolios.FirstOrDefault(IsSamePath(name));
            if (portfolio == null)
            {
                return RedirectToAction("Index");
            }

            return View(portfolio);
        }

        private Func<Portfolio, bool> IsSamePath(string name)
        {
            return p => string.Equals(p.Path, name, StringComparison.OrdinalIgnoreCase);
        }
    }
}