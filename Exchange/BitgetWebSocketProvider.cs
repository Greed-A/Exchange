using Bitget.Net.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange
{
    internal class BitgetWebSocketProvider
    {
        private readonly BitgetSocketClient socketClient;

        public event Action<decimal> OnPriceUpdate;

        public BitgetWebSocketProvider()
        {
            socketClient = new BitgetSocketClient();
        }

        public async Task StartWebSocketAsync()
        {
            await socketClient.SpotApi.SubscribeToTradeUpdatesAsync("BTCUSDT", data =>
            {
                OnPriceUpdate?.Invoke(data.Data.Last().Price);
            });
        }
    }
}
