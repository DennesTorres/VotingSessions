using System;
using System.Collections.Generic;
using System.Text;

namespace libVoting.CustomModels
{
    public class SimpleSession
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string SpeakerName { get; set; }

        public string TagLine { get; set; }

        public string ProfilePicture { get; set; }
    }
}
