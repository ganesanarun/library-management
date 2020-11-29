using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nest;
using Search_Server.ElasticSearch;

namespace Search_Server.Books
{
    public sealed class BookChangeEventHandler
    {
        private readonly ElasticClient client;
        private readonly ILogger<BookChangeEventHandler> logger;

        public BookChangeEventHandler(ElasticClient client, ILogger<BookChangeEventHandler> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task Handle(BookChangeEvent bookChangeEvent)
        {
            if (bookChangeEvent.Type == ChangeType.Deleted)
            {
                await client.DeleteAsync<Book>(bookChangeEvent.BookId, i => i.Index(Constants.IndexName));
                logger.LogInformation($"Deleted a book with an id: {bookChangeEvent.BookId}");
                return;
            }

            logger.LogInformation($"Updated a book: {bookChangeEvent.Data}");
            await client.IndexAsync(bookChangeEvent.Data!, i => i.Index(Constants.IndexName));
        }
    }
}