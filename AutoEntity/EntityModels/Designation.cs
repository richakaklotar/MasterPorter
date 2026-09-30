using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AutoEntity.EntityModels
{
    [Index(nameof(DesignationName), IsUnique = true)]
    public partial class Designation
    {
        [Key]
        public int DesignationID { get; set; }

        [Required(ErrorMessage = "Designation Name is required")]
        [StringLength(25, ErrorMessage = "Designation Name cannot exceed 25 characters")]
        public string DesignationName { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}