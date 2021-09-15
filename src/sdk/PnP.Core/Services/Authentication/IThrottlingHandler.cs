using System;
using System.Threading.Tasks;

namespace PnP.Core.Services.Authentication
{
    /// <summary>
    /// Interface for Throttling coodrination Handler
    /// </summary>
    public interface IThrottlingHandler
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="TenantId"></param>
        public void IThrottlingHandler(Guid TenantId);

        /// <summary>
        /// Will return when coordinated throttling state allows execution of request, keep pending requests in order and resolve them in a way that you not get imediatley throttled again
        /// </summary>
        /// <param name="apiRequestType"></param>
        /// <param name="ManagedThreadId"></param>
        /// <returns></returns>
        public Task<bool> WaitToExecuteRequest(ApiRequestType apiRequestType, int ManagedThreadId);

        /// <summary>
        /// Let throttling handler know when you got a throttling event 
        /// </summary>
        /// <param name="apiRequestType"></param>
        /// <param name="ManagedThreadId"></param>
        public void SetThrottlingState(ApiRequestType apiRequestType, int ManagedThreadId);
    }
}
