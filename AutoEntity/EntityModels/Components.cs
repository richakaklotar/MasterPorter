using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoEntity.EntityModels
{
    public partial class Components
    {
        [Key]
        public int ComponentID { get; set; }
        [Required]
        public string ComponentName { get; set; }
        [Required]
        public int StandardHours { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string SeriesNo { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectID { get; set; }

        [ForeignKey(nameof(Machine))]
        public int MachineID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Project Project { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Machine Machine { get; set; }
    }
}
