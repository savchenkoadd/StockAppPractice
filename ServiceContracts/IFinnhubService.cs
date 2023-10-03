namespace StocksApp.ServiceContracts
{
	public interface IFinnhubService
	{
		Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol = null!);
		Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol = null!);
	}
}
