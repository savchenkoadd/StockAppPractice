using StocksApp.Options;
using StocksApp.ServiceContracts;
using StocksApp.Services;

namespace StocksApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<IFinnhubService, FinnhubService>();
			builder.Services.AddHttpClient();
			builder.Services.Configure<FinnhubOptions>(
				builder.Configuration.GetSection("FinnhubOptions")
				); ;
			builder.Services.AddScoped<IStockTradeParsingService, StockTradeParsingService>();

			var app = builder.Build();

			app.UseStaticFiles();
			app.UseRouting();
			app.MapControllers();

			app.Run();
		}
	}
}