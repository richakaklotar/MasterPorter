using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoEntity.EntityModels
{
    [Index(nameof(ActivitiesName), IsUnique = true)]
    public partial class Activities
    {
        [Key]
        public int ActivitiesID { get; set; }

        [Required(ErrorMessage = "Activities Name is required")]
        [StringLength(25, ErrorMessage = "Activities Name cannot exceed 25 characters")]
        public string ActivitiesName { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [StringLength(25, ErrorMessage = "Type cannot exceed 25 characters")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Component is required")]
        [ForeignKey(nameof(Components))]
        public int ComponentID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Components Components { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}