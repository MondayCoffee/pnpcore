using PnP.Core.Services.Authentication;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PnP.Core.Services
{
    /// <summary>
    /// Defines the public interface that any Authentication Provider must implement
    /// </summary>
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Authenticates the specified request message.
        /// </summary>
        /// <param name="resource">Request uri</param>
        /// <param name="request">The <see cref="HttpRequestMessage"/> to authenticate.</param>
        /// <returns>The task to await.</returns>
        Task AuthenticateRequestAsync(Uri resource, HttpRequestMessage request);

        /// <summary>
        /// Gets an access token for the requested resource and scope
        /// </summary>
        /// <param name="resource">Resource to request an access token for</param>
        /// <param name="scopes">Scopes to request</param>
        /// <returns>An access token</returns>
        Task<string> GetAccessTokenAsync(Uri resource, String[] scopes);

        /// <summary>
        /// Gets an access token for the requested resource 
        /// </summary>
        /// <param name="resource">Resource to request an access token for</param>
        /// <returns>An access token</returns>
        Task<string> GetAccessTokenAsync(Uri resource);

        /// <summary>
        /// Will allow to coordinate throttling between multiple processes  running against same tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        delegate IThrottlingHandler ThrottlingHandler(Guid tenantId);
    }
}
