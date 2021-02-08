using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using libVoting.Models;
using System.Linq;
using libVoting.CustomModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace Voting
{
    public class Sessions
    {

        private readonly VotingContext votingDB;
        public Sessions(VotingContext voting) => votingDB = voting;

        [FunctionName("Sessions")]
        public async Task<IActionResult> GetSessions(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            var result = await ProjectSimpleSessions(votingDB.Sessions.Include(x => x.Speaker));

            return new OkObjectResult(result);
        }

        [FunctionName("SessionsBySpeaker")]
        public async Task<IActionResult> GetSessionsBySpeaker(
                [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Sessions/{speakerId}")] HttpRequest req,
                ILogger log,int speakerId)
        {

            var result = await ProjectSimpleSessions((from x in votingDB.Sessions.Include(x => x.Speaker)
                                                      where x.SpeakerId==speakerId
                                                      select x));                               

            return new OkObjectResult(result);
        }

        [FunctionName("SessionsByTitle")]
        public async Task<IActionResult> GetSessionsByTitle(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Sessions/{title}")] HttpRequest req,
        ILogger log, string title)
        {

            var result = await ProjectSimpleSessions((from x in votingDB.Sessions.Include(x => x.Speaker)
                                                      where x.Title.StartsWith(title)
                                                      select x));

            return new OkObjectResult(result);
        }

        [FunctionName("Voting")]
        public async Task<IActionResult> RegisterVote(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Sessions/Vote")] HttpRequest req,
            ILogger log, [libTools.Bindings.FromBody] VotesRecord votes)
        {


            //validation - total of votes
            var newVotes = votes.Sessions.Count();

            if (!string.IsNullOrEmpty(votes.UserKey))
            {
                var existingVotes = await (from v in votingDB.Voting
                                           where v.UserKey == votes.UserKey
                                           select v).CountAsync();

                if (newVotes + existingVotes > 5)
                {
                    return new BadRequestObjectResult("Vote limit would be exceded. Each user can register 5 votes");
                }

            }
            else votes.UserKey = Guid.NewGuid().ToString();

            var votesToInclude = libVoting.Models.Voting.CreateVotings(votes);
            votingDB.Voting.AddRange(votesToInclude);
            await votingDB.SaveChangesAsync();

            return new OkObjectResult( new VotingResult() { UserKey = votes.UserKey });
        }

        private async Task<List<SimpleSession>> ProjectSimpleSessions(IQueryable<libVoting.Models.Sessions> sessions) =>
            await (from s in sessions
            select new SimpleSession()
            {
                Id = s.Id,
                Description = s.Description,
                Title = s.Title,
                SpeakerName = s.Speaker.Name,
                ProfilePicture = s.Speaker.ProfilePicture,
                TagLine = s.Speaker.TagLine
            }).ToListAsync();
    }
}
