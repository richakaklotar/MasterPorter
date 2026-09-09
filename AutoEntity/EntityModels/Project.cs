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
    public partial class Project
    {
        [Key]
        public int ProjectID { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectCode { get; set; }
        [Required]
        public string Status { get; set; }

        [ForeignKey(nameof(Machine))]
        public int MachineID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Machine Machine { get; set; }
    }
}
