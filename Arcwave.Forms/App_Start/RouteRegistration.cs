using Sitecore.Pipelines;
using System.Web.Http;

namespace Arcwave.Forms.App_Start
{
    public class RouteRegistration
    {
        public virtual void Process(PipelineArgs args)
        {
            GlobalConfiguration.Configure(config =>
            {
                config.Routes.MapHttpRoute("ExternalFormEmail", "{api}/send-external-form-email", new
                {
                    controller = "SendExternalFormEmail",
                    action = "SendExternalFormEmail"
                });

                config.Routes.MapHttpRoute("GetContact", "{api}/get-contact-via-id", new
                {
                    controller = "SendExternalFormEmail",
                    action = "GetContact"
                });

                config.EnsureInitialized();
            });
        }
    }
}