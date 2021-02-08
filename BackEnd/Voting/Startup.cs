using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using libVoting.Models;
using Microsoft.EntityFrameworkCore;
using libTools.Bindings;
using Microsoft.Azure.WebJobs;
using Voting;

[assembly: FunctionsStartup(typeof(HttpTriggerVerify.Startup))]
namespace HttpTriggerVerify
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<VotingContext>(x => x.UseSqlServer(SqlConnection));


            // IWebJobsBuilders instance
            var wbBuilder = builder.Services.AddWebJobs(x => { return; });

            // And now you can use AddExtension
            wbBuilder.AddExtension<BindingExtensionProvider>();
        }
    }
}
