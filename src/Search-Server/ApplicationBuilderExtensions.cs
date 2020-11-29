using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Search_Server
{
    public static class ApplicationBuilderExtensions
    {
        private static EventConsumer? listener;

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            listener = app.ApplicationServices.GetService<EventConsumer>();
            var hostApplicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            hostApplicationLifetime?.ApplicationStarted.Register(OnStarted);
            return app;
        }

        private static void OnStarted()
        {
            listener?.Start();
        }
    }
}