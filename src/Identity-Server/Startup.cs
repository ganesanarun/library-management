using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity_Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddIdentityServer(options => { options.EmitStaticAudienceClaim = true; })
                .AddDeveloperSigningCredential(persistKey: false)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(TestUsers.Users);
        }

        public void Configure(IHostEnvironment env, IApplicationBuilder app)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseIdentityServer();
        }
    }
}