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
    public partial class SubActivities
    {
        [Key]
        public int SubActivitiesID { get; set; }
        [Required]
        public string SubActivitiesName { get; set; }

        [ForeignKey(nameof(Activities))]
        public int ActivitiesID { get; set; }

        [ForeignKey(nameof(Components))]
        public int ComponentID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Activities Activities { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Components Components { get; set; }
    }
}
