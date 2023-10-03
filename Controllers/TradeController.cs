using Microsoft.AspNetCore.Mvc;
using StocksApp.ServiceContracts;

namespace StocksApp.Controllers
{
	public class TradeController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly IStockTradeParsingService _stockParsingService;

        public TradeController(IFinnhubService finnhubService, IStockTradeParsingService stockParsingService)
        {
            _finnhubService = finnhubService;
			_stockParsingService = stockParsingService;
        }

        [Route("/{ticker?}")]
		[HttpGet]
		public async Task<IActionResult> Index(string? ticker)
		{
			Dictionary<string, object> companyProfile = new Dictionary<string, object>();
			Dictionary<string, object> stockTrade = new Dictionary<string, object>();

			if (!string.IsNullOrEmpty(ticker))
			{
				companyProfile = await _finnhubService.GetStockPriceQuote(ticker);
				stockTrade = await _finnhubService.GetCompanyProfile(ticker);
			}
			else
			{
				companyProfile = await _finnhubService.GetStockPriceQuote();
				stockTrade = await _finnhubService.GetCompanyProfile();
			}
			

			if (companyProfile is null || stockTrade is null)
			{
				throw new InvalidOperationException("Either company profile or stockTrade dictionary is not provided");
			}

			var resultDictionary = await _stockParsingService.Merge(companyProfile, stockTrade);

			return View(await _stockParsingService.Parse(resultDictionary));
		}
	}
}
