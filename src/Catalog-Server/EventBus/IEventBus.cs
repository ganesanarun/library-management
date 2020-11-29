using System.Threading.Tasks;

namespace Catalog_Server.EventBus
{
    public interface IEventBus<in T> where T : BaseMessage
    {
        Task Publish(T message);
    }
}