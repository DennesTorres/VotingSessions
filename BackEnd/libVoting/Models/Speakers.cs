using System;
using System.Collections.Generic;

namespace libVoting.Models
{
    public partial class Speakers
    {
        public Speakers()
        {
            Sessions = new HashSet<Sessions>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string ProfilePicture { get; set; }

        public virtual ICollection<Sessions> Sessions { get; set; }
    }
}
