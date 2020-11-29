using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;

namespace Identity_Server
{
    public class ProfileService : IProfileService
    {
        private readonly TestUserStore users;

        public ProfileService(TestUserStore users)
        {
            this.users = users;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject.Claims
                .FirstOrDefault(s => s.Type == JwtClaimTypes.Subject)
                ?.Value;
            try
            {
                var mayBeUser = users.FindBySubjectId(subject);
                if (mayBeUser == null) return Task.FromResult(0);

                var usernameClaim = new Claim("username", mayBeUser.Username);
                context.IssuedClaims.Add(usernameClaim);
                var mayBeRoleClaim = mayBeUser.Claims.FirstOrDefault(claim => claim.Type == JwtClaimTypes.Role);
                if (mayBeRoleClaim != null) context.IssuedClaims.Add(mayBeRoleClaim);

                return Task.FromResult(0);
            }
            catch
            {
                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}