using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace ImageNotification
{
    public static class SignalRNegotiate
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "hubName")] SignalRConnectionInfo connectionInfo)  /// UserId = "{headers.xxx}" add for security reasons but use Ibinder
        {
            ////binder.Bind<SignalRConnectionInfo>(new SignalRConnectionInfoAttribute() { HubName = "hubName", UserId = "ProfileId jwt token" });
            return connectionInfo;
        }
    }
}
