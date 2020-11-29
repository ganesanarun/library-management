using System;
using Catalog_Server.EventBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Catalog_Server.books
{
    public class BookChangeEvent : BaseMessage
    {
        [JsonConstructor]
        public BookChangeEvent(Guid eventId, ChangeType type, Guid bookId, Book? data = null) : base(eventId)
        {
            Type = type;
            Data = data;
            BookId = bookId;
        }

        public BookChangeEvent(Guid eventId, ChangeType type, Book data) : base(eventId)
        {
            Type = type;
            Data = data;
            BookId = data.Id;
        }

        [JsonProperty] public Guid BookId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty]
        public ChangeType Type { get; private set; }

        [JsonProperty] public Book? Data { get; private set; }
    }
}