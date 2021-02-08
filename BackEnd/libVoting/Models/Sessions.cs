using System;
using System.Collections.Generic;

namespace libVoting.Models
{
    public partial class Sessions
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? SpeakerId { get; set; }
        public int? ConferenceId { get; set; }
        public int OriginalId { get; set; }

        public virtual Conferences Conference { get; set; }
        public virtual Speakers Speaker { get; set; }

        public virtual ICollection<Voting> Votes { get; set; }
    }
}
