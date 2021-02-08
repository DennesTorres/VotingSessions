using libVoting.CustomModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace libVoting.Models
{
    public class Voting
    {
        public int Id { get; set; }
        public int SessionId { get; set; }

        public string UserKey { get; set; }
        public int ConferenceId { get; set; }
        public virtual Conferences Conference { get; set; }
        public virtual Sessions Session { get; set; }

        public static List<Voting> CreateVotings(VotesRecord votes)
        {
            List<Voting> votesToInclude = new List<Voting>();
            if (string.IsNullOrEmpty(votes.UserKey))
            {
                votes.UserKey = Guid.NewGuid().ToString();
            }
            foreach (var v in votes.Sessions)
            {
                var vt = new Voting()
                {
                    SessionId = v,
                    UserKey = votes.UserKey,
                    ConferenceId = votes.ConferenceId

                };

                votesToInclude.Add(vt);
            }
            return votesToInclude;
        }
    }
}
