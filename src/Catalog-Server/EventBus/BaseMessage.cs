using System;
using Newtonsoft.Json;

namespace Catalog_Server.EventBus
{
    public class BaseMessage
    {
        [JsonProperty] public Guid EventId { get; private set; }

        [JsonConstructor]
        public BaseMessage(Guid eventId)
        {
            EventId = eventId;
        }
    }
}