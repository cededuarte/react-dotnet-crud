using System;
using System.ComponentModel.DataAnnotations;


//CREATE NECESSARY COLUMNS FOR DATABASE
namespace aspnetserver.Data
{
    internal sealed class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [MaxLength(length: 1000)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(length: 1000)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(length: 15)]
        public string Contact { get; set; } = string.Empty;
    }
}

