namespace Exchange
{
    public interface IExchange
    {
        Task StartWebSocketAsync();
        event Action<decimal> OnPriceUpdate;
    }
}
