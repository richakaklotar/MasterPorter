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
    public partial class Activities
    {
        [Key]
        public int ActivitiesID { get; set; }
        [Required]
        public string ActivitiesName { get; set; }
        [Required]
        public string Type { get; set; }

        [ForeignKey(nameof(Components))]
        public int ComponentID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Components Components { get; set; }
    }
}
