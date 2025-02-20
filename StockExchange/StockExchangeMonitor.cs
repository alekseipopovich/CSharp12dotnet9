using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange
{
    public delegate void PriceChange(int price);
    class StockExchangeMonitor
    {
        // Handler Message - свойство для оповещения всех желающих
        public PriceChange PriceExchageHandler { get; set; }

        public void Start()
        {
            while (true)
            {
                int bankPrice = (new Random()).Next(100);
                
                PriceExchageHandler(bankPrice);

                Thread.Sleep(2000);
            }
        }
    }
}
