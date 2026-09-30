using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoEntity.EntityModels
{
    [Index(nameof(SubActivitiesName), IsUnique = true)]
    public partial class SubActivities
    {
        [Key]
        public int SubActivitiesID { get; set; }

        [Required(ErrorMessage = "Sub Activity Name is required")]
        [StringLength(25, ErrorMessage = "Sub Activity Name cannot exceed 25 characters")]
        public string SubActivitiesName { get; set; }

        [Required(ErrorMessage = "Activity is required")]
        [ForeignKey(nameof(Activities))]
        public int ActivitiesID { get; set; }

        [Required(ErrorMessage = "Component is required")]
        [ForeignKey(nameof(Components))]
        public int ComponentID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Activities Activities { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Components Components { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}