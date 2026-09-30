using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoEntity.EntityModels
{
    [Index(nameof(ProjectID), nameof(ComponentName), IsUnique = true)]
    [Index(nameof(ProjectID), nameof(SeriesNo), IsUnique = true)]
    public partial class Components
    {
        [Key]
        public int ComponentID { get; set; }

        [Required(ErrorMessage = "Component Name is required")]
        [StringLength(25, ErrorMessage = "Component Name cannot exceed 25 characters")]
        public string ComponentName { get; set; }

        [Required(ErrorMessage = "Standard Hours is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Standard Hours must be 0 or greater")]
        public int StandardHours { get; set; }

        [Required(ErrorMessage = "Top Hours is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Top Hours must be 0 or greater")]
        public int TopHours { get; set; }

        [Required(ErrorMessage = "Bottom Hours is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Bottom Hours must be 0 or greater")]
        public int BottomHours { get; set; }

        [Required(ErrorMessage = "Side Hours is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Side Hours must be 0 or greater")]
        public int SideHours { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be 0 or greater")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Series No is required")]
        [StringLength(25, ErrorMessage = "Series No cannot exceed 25 characters")]
        public string SeriesNo { get; set; }

        [Required(ErrorMessage = "Project is required")]
        [ForeignKey(nameof(Project))]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Machine is required")]
        [ForeignKey(nameof(Machine))]
        public int MachineID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Project Project { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Machine Machine { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}