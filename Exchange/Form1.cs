namespace Exchange
{
    public partial class Form1 : Form
    {
        private BinanceWebSocketProvider binanceWebSocketProvider;
        //private BybitWebSocketProvider bybitWebSocketProvider;
        private KucoinWebSocketProvider kucoinWebSocketProvider;
        //private BitgetWebSocketProvider bitgetWebSocketProvider;

        private decimal currentPrice;

        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("Binance");
            comboBox1.Items.Add("Kucoin");

            binanceWebSocketProvider = new BinanceWebSocketProvider();
            //bybitWebSocketProvider = new BybitWebSocketProvider();
            kucoinWebSocketProvider = new KucoinWebSocketProvider();
            //bitgetWebSocketProvider = new BitgetWebSocketProvider();

            binanceWebSocketProvider.OnPriceUpdate += OnBinancePriceUpdate;
            //bybitWebSocketProvider.OnPriceUpdate += OnBybitPriceUpdate;
            kucoinWebSocketProvider.OnPriceUpdate += OnKucoinPriceUpdate;
            //bitgetWebSocketProvider.OnPriceUpdate += OnBitgetPriceUpdate;

            StartWebSocketUpdates();
        }

        private async void StartWebSocketUpdates()
        {
            await Task.WhenAll(
                binanceWebSocketProvider.StartWebSocketAsync(),
                //bybitWebSocketProvider.StartWebSocketAsync(),
                kucoinWebSocketProvider.StartWebSocketAsync()
                //bitgetWebSocketProvider.StartWebSocketAsync()
                );

            // Запустить таймер для обновления курсов каждые 5 секунд
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += async (sender, e) =>
            {
                await RefreshPrices();
            };
            timer.Interval = 0;
            timer.Start();
        }

        private async Task RefreshPrices()
        {
            await Task.WhenAll(
                binanceWebSocketProvider.StartWebSocketAsync(),
                //bybitWebSocketProvider.StartWebSocketAsync(),
                kucoinWebSocketProvider.StartWebSocketAsync()
                //bitgetWebSocketProvider.StartWebSocketAsync()
                );
            await Task.Delay(5000);
        }

        private void OnBinancePriceUpdate(decimal price)
        {
            label3.Invoke((MethodInvoker)(() => label3.Text = $"Binance: {price}"));
        }

        private void OnKucoinPriceUpdate(decimal price)
        {
            label3.Invoke((MethodInvoker)(() => label3.Text = $"Kucoin: {price}"));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = "";
        }
    }
}