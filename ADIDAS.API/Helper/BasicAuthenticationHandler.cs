//------------------------------------------------------------------------------
// <copyright file="BasicAuthenticationHandler.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Helper
{
    using System;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Basic Authentication Handler.
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        /// <summary>
        /// User Service.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthenticationHandler"/> class.
        /// Basic Authentication Handler.
        /// </summary>
        /// <param name="options">options.</param>
        /// <param name="logger">logger.</param>
        /// <param name="encoder">encoder.</param>
        /// <param name="clock">clock.</param>
        /// <param name="userService">userService.</param>
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService)
            : base(options, logger, encoder, clock)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Handle Authenticate Async.
        /// </summary>
        /// <returns>Authenticate Result.</returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!this.Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
            }

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(this.Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);

                if (credentials[0] == "adidaas" && credentials[1] == "Disys@321")
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, "ADIDAAS_API_ID"),
                        new Claim(ClaimTypes.Name, "ADIDAAS_API_USER"),
                    };
                    var identity = new ClaimsIdentity(claims, this.Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
                else
                {
                    return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));
                }
            }
            catch
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }
        }
    }
}
