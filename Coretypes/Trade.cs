using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coretypes
{
    public class Trade
    {

        public int Quantity { get; set; }
        public DateTime TradedOn { get; set; }
        public BuySell BuySell { get; set; }
        public Double Price { get; set; }
        public string Identifier { get; set; }
        public Trade(string identifier, DateTime dateTime, BuySell buySell, double price, int quantity)
        {
            Identifier = identifier;
            TradedOn = dateTime;
            BuySell = buySell;
            Price = price;
            Quantity = quantity;
        }

        public Trade()
        {
        }
    }
}
