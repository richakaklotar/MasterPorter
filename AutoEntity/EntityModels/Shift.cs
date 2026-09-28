using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEntity.EntityModels
{
    public partial class Shift
    {
        [Key]
        public int ShiftID { get; set; }
        [Required]
        public string ShiftName { get; set; }
        [Required]
        [NotMapped]
        public TimeOnly StartTime { get; set; }
        [Required]
        [NotMapped]
        public TimeOnly EndTime { get; set; }
        public string Status { get; set; }
    }
}
