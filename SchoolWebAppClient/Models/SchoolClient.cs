using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SchoolWebAppClient.Models
{
    public class SchoolClient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Sections { get; set; }

        [Required]
        public string Director { get; set; }

        public double Rating { get; set; }

        public string? WebSite { get; set; }
    }
}



