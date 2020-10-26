using System.Security.Claims;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Identity.CustomIdentityDB.Factories
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<CustomIdentityUser>
    {
        public CustomUserClaimsPrincipalFactory(UserManager<CustomIdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(CustomIdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("locale", user.Locale));

            return identity;
        }
    }
}