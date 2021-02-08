using libVoting.CustomModels;
using libVoting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Voting
{
    public class Speakers
    {

        private readonly VotingContext votingDB;
        public Speakers(VotingContext voting) => votingDB = voting;

        [FunctionName("Speakers")]
        public async Task<IActionResult> GetSpeakers(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var result = await (from s in votingDB.Speakers
                          select new SimpleSpeaker()
                          { Code = s.Code, Id = s.Id, Name = s.Name }).ToListAsync();

            return new OkObjectResult(result);
        }
    }
}
