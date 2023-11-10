using Bybit.Net.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange
{
    internal class BybitWebSocketProvider
    {
        private readonly BybitSocketClient socketClient;

        public event Action<decimal> OnPriceUpdate;

        public BybitWebSocketProvider()
        {
            socketClient = new BybitSocketClient();
        }

        public async Task StartWebSocketAsync()
        {
            await socketClient.SpotV3Api.SubscribeToTradeUpdatesAsync("BTCUSDT", data =>
            {
                OnPriceUpdate?.Invoke(data.Data.Price);
            });
        }
    }
}
