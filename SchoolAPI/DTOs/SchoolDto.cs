using System.ComponentModel.DataAnnotations;


namespace SchoolAPI.DTOs
{
    public class SchoolDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        
        public string City { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }
    }
}
