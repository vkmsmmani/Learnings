
namespace Subbu.Learning.Plugins
{
    using System;
    using Microsoft.Xrm.Sdk;
   
    /// <summary>
    /// Contact - Plugin
    /// </summary>
    public class Contact : IPlugin
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
                if (target.Contains("emailaddress1"))
                {
                    Entity preImage = (Entity) context.PreEntityImages["PreImage"],
                           postImage = (Entity) context.PostEntityImages["PostImage"];
                    Entity updatingEntity = new Entity("contact");
                    updatingEntity.Id = target.Id;
                    updatingEntity["description"] = "Pre Image: Emailaddress1 - " + preImage.GetAttributeValue<string>("emailaddress1") + " Post Image: Emailaddress1 - " + postImage.GetAttributeValue<string>("emailaddress1");
                    service.Update(updatingEntity);
                    
                }
                if (target.Contains("mobilephone"))
                {
                    Entity account = new Entity("account");
                    account["primarycontactid"] = target.ToEntityReference();
                    account["name"] = target["mobilephone"];
                    service.Create(account);
                }
                
            }
            catch (Exception exception)
            {
                throw new InvalidPluginExecutionException(exception.Message);
            }
        }
    }
}
