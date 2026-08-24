using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEntity.EntityModels
{
    public partial class Machine
    {
        [Key]
        public int MachineID { get; set; }
        [Required]
        public string MachineName { get; set; }
        [Required]
        public string MachineCode { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
