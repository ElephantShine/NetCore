using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elephantshine.Models.Enum;

namespace elephantshine.Models.ViewModel
{
    public class PortfolioViewModel
    {
        public List<Portfolio> Portfolios { get; }
        public List<EnumCategory> EnableCategorys { get; }

        public PortfolioViewModel(
            List<Portfolio> portfolios, 
            List<EnumCategory> enableCategorys, 
            Settings settings)
        {
            portfolios.ForEach(a => a.BaseUrl = settings.BaseUrl);
            Portfolios = portfolios;
            EnableCategorys = enableCategorys;
        }
    }
}
