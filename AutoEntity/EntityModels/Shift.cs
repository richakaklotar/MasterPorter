using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoEntity.EntityModels
{
    [Index(nameof(ShiftName), IsUnique = true)]
    public partial class Shift
    {
        [Key]
        public int ShiftID { get; set; }

        [Required(ErrorMessage = "Shift Name is required")]
        [StringLength(25, ErrorMessage = "Shift Name cannot exceed 25 characters")]
        public string ShiftName { get; set; }

        [Required(ErrorMessage = "Start Time is required")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "End Time is required")]
        public TimeOnly EndTime { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}