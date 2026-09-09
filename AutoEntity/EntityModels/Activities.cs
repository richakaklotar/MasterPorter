using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [Required]
        public string Status { get; set; }
    }
}