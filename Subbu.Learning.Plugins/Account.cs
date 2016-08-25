

namespace Subbu.Learning.Plugins
{
    using System;
    using Microsoft.Xrm.Sdk;

    public class Account : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                if (serviceProvider == null)
                {
                    throw new ArgumentNullException("serviceProvider", "Service Provider is Null");
                }

                IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                IOrganizationService service = factory.CreateOrganizationService(context.UserId);
                ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
                Entity target = (Entity)context.InputParameters["Target"];
                Entity contact = new Entity("contact");
                contact.Id = (target.GetAttributeValue<EntityReference>("primarycontactid")).Id;
                contact["emailaddress1"] = target.GetAttributeValue<string>("name") + "@subbu.learnings.com";
                service.Update(contact);
            }
            catch (Exception exception)
            {
                throw new InvalidPluginExecutionException(exception.Message);
            }
        }
    }
}
