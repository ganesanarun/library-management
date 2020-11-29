using System.Runtime.Serialization;

namespace Search_Server.ElasticSearch
{
    public enum ChangeType
    {
        [EnumMember(Value = "Book Created")] Created = 1,
        [EnumMember(Value = "Book Updated")] Updated = 2,
        [EnumMember(Value = "Book Deleted")] Deleted,
    }
}