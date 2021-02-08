using HttpTriggerVerify;
using libTools.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Voting
{
    public class BindingExtensionProvider : IExtensionConfigProvider
    {
        private readonly ILogger logger;
        public BindingExtensionProvider(ILogger<Startup> logger)
        {
            this.logger = logger;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            // Creates a rule that links the attribute to the binding
            context.AddBindingRule<FromBodyAttribute>().Bind(new FromBodyBindingProvider(this.logger));
        }
    }
}
