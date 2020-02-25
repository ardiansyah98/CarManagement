using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarManagement.Models
{
    public class User
    {
        [Display(Name = "Email")]
        [Column(TypeName = "nvarchar(100)")]
        [Required]
        [Key]
        public string szEmail { get; set; }
        [Display(Name = "Full Model")]
        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string szFullName { get; set; }
        [Display(Name = "Password")]
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public DateTime dtmCreated { get; set; }
        [Required]
        public DateTime dtmUpdated { get; set; }

    }
}
