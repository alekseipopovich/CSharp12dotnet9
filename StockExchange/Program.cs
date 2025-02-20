using StockExchange;

StockExchangeMonitor stockExchangeMonitor = new StockExchangeMonitor();

stockExchangeMonitor.PriceExchageHandler = ShowPrice;

stockExchangeMonitor.Start();

void ShowPrice(int price)
{
    Console.WriteLine($"New price is {price}");
}