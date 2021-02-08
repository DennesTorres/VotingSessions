using System;
using System.Collections.Generic;

namespace libVoting.Models
{
    public partial class Conferences
    {
        public Conferences()
        {
            Sessions = new HashSet<Sessions>();
        }

        public int Id { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Sessions> Sessions { get; set; }

        public virtual ICollection<Voting> Votes { get; set; }
    }
}
