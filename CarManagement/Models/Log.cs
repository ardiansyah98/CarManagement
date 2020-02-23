using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarManagement.Models
{
    public class Log
    {
        [Column(TypeName = "binary(16)")]
        [Key]
        public Guid gdHistoryId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string szUri { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string szEmail { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        [Required]
        public string szAction { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [Required]
        public string szData { get; set; }

        [Required]
        public DateTime dtmCreated { get; set; }
    }
}
