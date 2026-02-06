using Azure.Core;
using HRIS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Services
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private IOptionsMonitor<BasicAuthenticationOptions> _options;
        private readonly HrisDbContext _dbContext;
        public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, HrisDbContext dbContext, ILoggerFactory log, UrlEncoder encoder, ISystemClock clock) : base(options, log, encoder, clock)
        {
            _options = options;
            _dbContext = dbContext;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Console.WriteLine("Handler Calling");

            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var headerValue))
            {
                return AuthenticateResult.NoResult();
            }

            var auth = Request.Headers["Authorization"].ToString();
            var userToken = await _dbContext.ApiTokenModels
                .Where(t => t.ApiToken == auth && t.Status == 1)
                .FirstOrDefaultAsync();

            if (userToken != null)
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, userToken.Name),
            new Claim(ClaimTypes.Role, userToken.Role)
             };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.NoResult();
        }
    }
}
