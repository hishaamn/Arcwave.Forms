using Arcwave.Forms.Models;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.EmailCampaign.Model.Messaging;
using Sitecore.EmailCampaign.Model.Messaging.Buses;
using Sitecore.Framework.Messaging;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Arcwave.Forms.Controllers
{
    [EnableCors("*", "*", "*")]
    [ServicesController]
    public class SendExternalFormEmailController : ServicesApiController
    {
        [HttpPost]
        public IHttpActionResult SendExternalFormEmail(EmailData data)
        {
            IMessageBus<AutomatedMessagesBus> automatedMessageBus = ServiceLocator.ServiceProvider.GetService<IMessageBus<AutomatedMessagesBus>>();

            var contact = new ContactIdentifier(data.Source, data.ContactIdentifier, ContactIdentifierType.Known);

            var dictionary = new Dictionary<string, object>();

            foreach (var formData in data.FormData)
            {
                dictionary.Add(formData.Key, formData.Value);
            }

            var automatedMessage = new AutomatedMessage()
            {
                ContactIdentifier = contact,
                MessageId = new Guid(data.EmailCampaignId),
                CustomTokens = dictionary,
                TargetLanguage = Sitecore.Context.Language.Name
            };

            automatedMessageBus.Send(automatedMessage);

            var status = new Status
            {
                StatusCode = "Status 200"
            };

            var apiResponse = new ApiResponse<Status>
            {
                Result = status,
                StatusCode = "Api Status 200"
            };

            return Json(new { status = HttpStatusCode.OK, response = apiResponse });
        }

        [HttpPost]
        public IHttpActionResult GetContact(ContactData contactData)
        {
            using (XConnectClient client = SitecoreXConnectClientConfiguration.GetClient())
            {
                try
                {
                    var reference = new ContactReference(Guid.Parse(contactData.ContactId));

                    Contact contact = client.Get<Contact>(reference, new ContactExpandOptions() { });

                    return Json(new { status = HttpStatusCode.OK, contact });
                }
                catch (XdbExecutionException ex)
                {
                    // Manage exceptions
                }
            }

            return Json(new { status = HttpStatusCode.OK, message = true });
        }
    }
}