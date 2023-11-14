namespace Exchange
{
    public partial class Form1 : Form
    {
        private IExchange binanceWebSocketProvider;
        private IExchange bybitWebSocketProvider;
        private IExchange kucoinWebSocketProvider;
        private IExchange bitgetWebSocketProvider;

        private decimal binancePrice;
        private decimal kucoinPrice;
        private decimal bybitPrice;
        private decimal bitgetPrice;

        public Form1()
        {
            InitializeComponent();

            binanceWebSocketProvider = new BinanceWebSocketProvider();
            bybitWebSocketProvider = new BybitWebSocketProvider();
            kucoinWebSocketProvider = new KucoinWebSocketProvider();
            bitgetWebSocketProvider = new BitgetWebSocketProvider();

            binanceWebSocketProvider.OnPriceUpdate += OnBinancePriceUpdate;
            bybitWebSocketProvider.OnPriceUpdate += OnBybitPriceUpdate;
            kucoinWebSocketProvider.OnPriceUpdate += OnKucoinPriceUpdate;
            bitgetWebSocketProvider.OnPriceUpdate += OnBitgetPriceUpdate;

            Updating();
        }
        private void Updating()
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += async (sender, e) =>
            {
                await RefreshPrices();
                label2.Text = $"Kucoin: {kucoinPrice}";
                label3.Text = $"Binance: {binancePrice}";
                label1.Text = $"Bybit: {bybitPrice}";
                label4.Text = $"Bitget: {bitgetPrice}";
            };
            timer.Interval = 5000;
            timer.Start();
        }

        private async Task RefreshPrices()
        {
            await Task.WhenAll(
                binanceWebSocketProvider.StartWebSocketAsync(),
                bybitWebSocketProvider.StartWebSocketAsync(),
                kucoinWebSocketProvider.StartWebSocketAsync(),
                bitgetWebSocketProvider.StartWebSocketAsync()
                );
        }

        private void OnBinancePriceUpdate(decimal price)
        {
            binancePrice = price;
        }

        private void OnKucoinPriceUpdate(decimal price)
        {
            kucoinPrice = price;
        }

        private void OnBybitPriceUpdate(decimal price)
        {
            bybitPrice = price;
        }

        private void OnBitgetPriceUpdate(decimal price)
        {
            bitgetPrice = price;
        }
    }
}