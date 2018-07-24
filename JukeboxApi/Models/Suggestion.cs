using System;

namespace JukeboxApi.Models
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string SubmitterName { get; set; }

        public string SongName { get; set; }

        public string ArtistName { get; set; }

        public DateTime Added { get; set; }
    }
}