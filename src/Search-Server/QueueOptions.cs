namespace Search_Server
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class QueueOptions
    {
        public const string QueueSection = "Search:RabbitMq";
        public const string ConnectionUrlSection = QueueSection + ":ConnectionUrl";

        public string Queue { get; set; } = null!;
    }
}