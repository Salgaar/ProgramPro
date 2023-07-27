using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProgramPro.Shared.Models;

namespace ProgramPro.Server.Data
{
    public class IdentityContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public IdentityContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}