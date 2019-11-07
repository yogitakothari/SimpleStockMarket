using Coretypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public static class StocksHelper
    {
        public static double GetGeometricMean(Dictionary<Stock, List<Trade>> trades)
        {
            try
            {
                if (trades != null && trades.Count > 0)
                {
                    double product = 1;
                    trades.ToList().ForEach(x => product = product * x.Key.GetVolumeWeightedStockPrice(x.Value));
                    product = Math.Pow(product, trades.Count);
                    return product;

                }
                return 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
