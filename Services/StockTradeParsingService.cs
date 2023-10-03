using StocksApp.Models;
using StocksApp.ServiceContracts;
using System.Linq;

namespace StocksApp.Services
{
	public class StockTradeParsingService : IStockTradeParsingService
	{
		private List<string> _requeredKeys = new List<string>()
		{
			"name", "ticker", "c"
		};

		public Task<Dictionary<string, object>> Merge(Dictionary<string, object> dictionaryFrom, Dictionary<string, object> dictionaryTo)
		{
			return Task.Run(() => {
				return dictionaryTo.Union(dictionaryFrom).ToDictionary(k => k.Key, v => v.Value);
				});
		}

		public async Task<StockTrade> Parse(Dictionary<string, object> data)
		{
			if (!(await ValidateKeys(data)))
			{
				throw new ArgumentException("Parsing data has not been provided");
			}

			StockTrade result = new StockTrade()
			{
				Price = Convert.ToDouble(data["c"].ToString()),
				StockName = data["name"].ToString(),
				StockSymbol = data["ticker"].ToString(),
			};

			return result;
		}

		private async Task<bool> ValidateKeys(Dictionary<string, object> data)
		{
			return await Task.Run(() =>
			{
				bool result = true;

				foreach (var key in _requeredKeys)
				{
					if (!data.ContainsKey(key))
					{
						result = false;
					}
				}

				return result;
			});
		}
	}
}
