using Binance.Net.Clients;
using Kucoin.Net.Clients;

namespace Exchange
{
    public class BinanceWebSocketProvider : IExchange
    {
        private readonly BinanceSocketClient socketClient;

        public event Action<decimal> OnPriceUpdate;

        public BinanceWebSocketProvider()
        {
            socketClient = new BinanceSocketClient();
        }

        public async Task StartWebSocketAsync()
        {
            await socketClient.SpotApi.ExchangeData.SubscribeToTradeUpdatesAsync("BTCUSDT", data =>
            {
                OnPriceUpdate?.Invoke(data.Data.Price);
            });
        }

        public string GetName()
        {
            return "Binance";
        }
    }

}
