using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coretypes
{
   
    public class Stock
    {
        public string Indicator { get; set; }
        public double Dividend { get; set; }
        public double LastDividend { get; set; }
        public double FixedDividend { get; set; }
        public double ParValue { get; set; }
        public Type Type { get; set; }
        

        
        public double CalculateDividendYield(double price)
        {
            try
            {
                double dividendYield = 0;
                if (price != 0)
                {
                    switch (Type)
                    {
                        case Type.Common:
                            dividendYield = LastDividend / price;
                            break;
                        default:
                            dividendYield = (FixedDividend * ParValue) / price;
                            break;
                    }
                }
                return dividendYield;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public double CalculatePERatio(double price)
        {
            try
            {
                double dividendYield = CalculateDividendYield(price);
                if (dividendYield != 0)
                {
                    return price / dividendYield;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }
      
        public double GetVolumeWeightedStockPrice(List<Trade> trades)
        {
            try
            {
                double sp = 0;
                var newTrades = trades.Where(x => x.TradedOn >= DateTime.Now.AddMinutes(-10)).ToList();
                newTrades.ForEach(x =>sp = sp + (x.Price * x.Quantity));
                var final = sp/(newTrades.Sum(x => x.Quantity));
                return final;
            }
            catch (Exception)
            {

                throw;
            }
        }
        

    }
}
