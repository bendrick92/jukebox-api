using System;
using System.ComponentModel.DataAnnotations;

namespace JukeboxApi.Models
{
    public class SuggestionDto
    {
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string SubmitterName { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string SongName { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string ArtistName { get; set; }

        [Required]
        public string Recaptcha { get; set; }
    }
}