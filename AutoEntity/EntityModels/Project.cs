using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoEntity.EntityModels
{
    [Index(nameof(ProjectName), IsUnique = true)]
    [Index(nameof(ProjectCode), IsUnique = true)]
    public partial class Project
    {
        [Key]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Project Name is required")]
        [StringLength(25, ErrorMessage = "Project Name cannot exceed 25 characters")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Project Code is required")]
        [StringLength(25, ErrorMessage = "Project Code cannot exceed 25 characters")]
        public string ProjectCode { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Machine is required")]
        [ForeignKey(nameof(Machine))]
        public int MachineID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Machine Machine { get; set; }
    }
}