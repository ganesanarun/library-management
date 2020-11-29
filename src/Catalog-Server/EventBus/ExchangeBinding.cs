namespace Catalog_Server.EventBus
{
    public class ExchangeBinding
    {
        public ExchangeBinding(string exchange, string routingKey)
        {
            Exchange = exchange;
            RoutingKey = routingKey;
        }

        public string Exchange { get; }

        public string RoutingKey { get; }
    }
}