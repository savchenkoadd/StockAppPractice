using StocksApp.Models;

namespace StocksApp.ServiceContracts
{
	public interface IStockTradeParsingService
	{
		Task<StockTrade> Parse(Dictionary<string, object> data);

		Task<Dictionary<string, object>> Merge(Dictionary<string, object> dictionaryFrom, Dictionary<string, object> dictionaryTo);
	}
}
