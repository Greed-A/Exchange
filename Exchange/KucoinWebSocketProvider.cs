using Kucoin.Net.Clients;

namespace Exchange
{
    public class KucoinWebSocketProvider : IExchange
    {
        private readonly KucoinSocketClient socketClient;

        public event Action<decimal> OnPriceUpdate;

        public KucoinWebSocketProvider()
        {
            socketClient = new KucoinSocketClient();
        }

        public async Task StartWebSocketAsync()
        {
            await socketClient.SpotApi.SubscribeToTradeUpdatesAsync("BTC-USDT", data =>
            {
                OnPriceUpdate?.Invoke(data.Data.Price);
            });
        }
    }

}
