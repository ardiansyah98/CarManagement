using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarManagement.Models
{
    public class Car
    {
        [Display(Name = "Car Id")]
        [Column(TypeName = "nvarchar(50)")]
        [Key]
        public string szCarId { get; set; }
        [Display(Name = "Car Name")]
        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string szCarName { get; set; }
        [Display(Name = "Car Model")]
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string szCarModel { get; set; }
        [Required]
        public DateTime dtmCreated { get; set; }
        [Required]
        public DateTime dtmUpdated { get; set; }
    }
}
