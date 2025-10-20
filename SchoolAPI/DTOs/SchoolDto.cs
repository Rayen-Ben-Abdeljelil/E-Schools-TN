using System.ComponentModel.DataAnnotations;


namespace SchoolAPI.DTOs
{
    public class SchoolDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }
    }
}
