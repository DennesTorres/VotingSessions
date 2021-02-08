using System;
using System.Collections.Generic;
using System.Text;

namespace libVoting.CustomModels
{
    public class VotesRecord
    {
        public string UserKey { get; set; }
        public int ConferenceId { get; set; }

        public List<int> Sessions { get; set; }

    }
}
