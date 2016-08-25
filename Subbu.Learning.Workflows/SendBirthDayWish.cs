
namespace Subbu.Learning.Workflows
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Workflow;
    using System;
    using System.Activities;

    public class SendBirthDayWish : CodeActivity
    {
        [RequiredArgument]
        [Input("Contact")]
        [ReferenceTarget("contact")]
        public InArgument<EntityReference> inputContact { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)executionContext.GetExtension<IOrganizationServiceFactory>();
            ITracingService tracingService = (ITracingService)executionContext.GetExtension<ITracingService>();
            IOrganizationService organizationService = factory.CreateOrganizationService(context.UserId);

            Entity contact = new Entity("contact");
            contact.Id = inputContact.Get<EntityReference>(executionContext).Id;
            contact["custom_sendbirthdaywish"] = true;
            organizationService.Update(contact);
        }
    }
}
