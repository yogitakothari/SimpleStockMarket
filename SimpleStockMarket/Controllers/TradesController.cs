using BusinessLogic;
using Coretypes;
using SimpleStockMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleStockMarket.Controllers
{
    public class TradesController : ApiController
    {
        // GET api/values

        [Route("api/Trades/Get")]
        public List<Stock> Get()
        {
            return SampleData.stocks;
        }

        // GET api/values/5
        [Route("api/Trades/GetTrades")]
        public Dictionary<string, List<Trade>> GetTrades(string identifier)
        {
            return TradesExecuted.GetByIdentifier(identifier);
        }

        [HttpGet]
        [Route("api/Trades/AddSellTrade")]
        public List<Trade> AddSellTrade(string identifier, double price, int quantity)
        {
            TradesExecuted.AddTrade(identifier, DateTime.Now, BuySell.Sell, price, quantity);
            return TradesExecuted.Trades;
        }
        [HttpGet]
        [Route("api/Trades/AddBuyTrade")]
        public List<Trade> AddBuyTrade(string identifier, double price, int quantity)
        {
            TradesExecuted.AddTrade(identifier, DateTime.Now, BuySell.Buy, price, quantity);
            return TradesExecuted.Trades;
        }
        
        [Route("api/Trades/GetDividendYield")]
        public double GetDividendYield(string identifier, double price)
        {
            Stock s = SampleData.stocks.Where(x => x.Indicator.ToLower() == identifier.ToLower()).FirstOrDefault();
            if (s != null)
            {
                return s.CalculateDividendYield(price);
            }
            return 0;
        }
        
        [Route("api/Trades/GetPERatio")]
        public double GetPERatio(string identifier, double price)
        {
            Stock s = SampleData.stocks.Where(x => x.Indicator.ToLower() == identifier.ToLower()).FirstOrDefault();
            if (s != null)
            {
                return s.CalculatePERatio(price);
            }
            return 0;
        }
        
        [Route("api/Trades/GetVWSP")]
        public double GetVWSP(string identifier)
        {
            Stock s = SampleData.stocks.Where(x => x.Indicator.ToLower() == identifier.ToLower()).FirstOrDefault();
            var trades = TradesExecuted.GetByIdentifier(identifier);
            if (trades != null)
            {
                return s.GetVolumeWeightedStockPrice(trades.FirstOrDefault().Value);
            }
            return 0;
        }

        
        [Route("api/Trades/GetGM")]
        public double GetGM()
        {
            Dictionary<Stock, List<Trade>> tradesByStocks = TradesExecuted.GetByStock();
            return StocksHelper.GetGeometricMean(tradesByStocks);
        }

       

    }
}
