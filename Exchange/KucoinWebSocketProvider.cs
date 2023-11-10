using CryptoExchange.Net.Interfaces;
using Kucoin.Net.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string GetName()
        {
            return "Kucoin";
        }
    }

}
