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
        [ForeignKey(nameof(Plant))]
        public int PlantId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Plant Plant { get; set; }
        [ForeignKey(nameof(Division))]
        public int DivisionId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Division Division { get; set; }
    }
}
