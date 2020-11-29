using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Search_Server.ElasticSearch;

namespace Search_Server.Books
{
    public class BookChangeEvent
    {
        [JsonConstructor]
        public BookChangeEvent(Guid eventId, string version, Guid bookId, ChangeType type, Book? data)
        {
            EventId = eventId;
            Version = version;
            BookId = bookId;
            Type = type;
            Data = data;
        }

        [JsonProperty] public Guid EventId { get; private set; }

        [JsonProperty] public string Version { get; private set; }

        [JsonProperty] public Guid BookId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty]
        public ChangeType Type { get; private set; }

        [JsonProperty] public Book? Data { get; private set; }
    }
}