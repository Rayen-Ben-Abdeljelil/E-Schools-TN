using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SchoolAPI.Models
{
    public class School
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Sections { get; set; }

        [Required]
        public string Director { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

        [Url]
        public string? WebSite { get; set; }
    }
}
        
    

