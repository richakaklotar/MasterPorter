using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
    }
}
