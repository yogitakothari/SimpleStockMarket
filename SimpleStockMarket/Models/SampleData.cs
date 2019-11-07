using Coretypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleStockMarket.Models
{
    public class SampleData
    {
        public static List<Stock> stocks = new List<Stock>() {

            new Stock() { Indicator = "TEA", Type = Coretypes.Type.Common, FixedDividend = 0, LastDividend = 0, ParValue = 100 },
            new Stock() { Indicator = "POP", Type = Coretypes.Type.Common, FixedDividend = 0, LastDividend = 8, ParValue = 100 },
            new Stock() { Indicator = "ALE", Type = Coretypes.Type.Common, FixedDividend = 0, LastDividend = 23, ParValue = 60 },
            new Stock() { Indicator = "GIN", Type = Coretypes.Type.Preferred, FixedDividend = 2, LastDividend = 8, ParValue = 100 },
        new Stock() { Indicator = "JOE", Type = Coretypes.Type.Common, FixedDividend = 0, LastDividend = 13, ParValue = 250 } };
    }

    public static class TradesExecuted
    {
        public static List<Trade> Trades
        {
            get; set;
        }

        public static void AddTrade(string identifier, DateTime dateTime, BuySell buySell, double price, int quantity)
        {
            try
            {
                Trade trade = new Trade(identifier, dateTime, buySell, price, quantity);
                if (Trades == null)
                {
                    Trades = new List<Trade>();
                }
                Trades.Add(trade);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static Dictionary<string, List<Trade>> GetByIdentifier(string identifier = null)
        {
            try
            {
                Dictionary<string, List<Trade>> stockWiseTrades = null;
                if (Trades != null && Trades.Count > 0)
                    if (!string.IsNullOrEmpty(identifier))
                    {
                        stockWiseTrades = Trades.Where(x => x.Identifier.ToLower() == identifier.ToLower()).GroupBy(x => x.Identifier).ToDictionary(x => x.Key, x => x.ToList());
                    }
                    else
                    {
                        stockWiseTrades = Trades.GroupBy(x => x.Identifier).ToDictionary(x => x.Key, x => x.ToList());
                    }
                return stockWiseTrades;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static Dictionary<Stock, List<Trade>> GetByStock()
        {
            try
            {
                Dictionary<Stock, List<Trade>> stockWiseTrades = new Dictionary<Stock, List<Trade>>();

                {
                    var byIdentifier = GetByIdentifier();
                    foreach (var identifier in byIdentifier)
                    {
                        var stock = SampleData.stocks.Where(y => y.Indicator.ToLower().Contains(identifier.Key.ToLower())).FirstOrDefault();
                        if (stock != null)
                        {
                            stockWiseTrades.Add(stock, identifier.Value);
                        }
                    }
                }
                return stockWiseTrades;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}