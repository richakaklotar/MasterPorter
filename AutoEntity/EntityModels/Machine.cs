using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoEntity.EntityModels
{
    [Index(nameof(MachineName), IsUnique = true)]
    [Index(nameof(MachineCode), IsUnique = true)]
    public partial class Machine
    {
        [Key]
        public int MachineID { get; set; }

        [Required(ErrorMessage = "Machine Name is required")]
        [StringLength(25, ErrorMessage = "Machine Name cannot exceed 25 characters")]
        public string MachineName { get; set; }

        [Required(ErrorMessage = "Machine Code is required")]
        [StringLength(25, ErrorMessage = "Machine Code cannot exceed 25 characters")]
        public string MachineCode { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Plant is required")]
        [ForeignKey(nameof(Plant))]
        public int PlantId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Plant Plant { get; set; }

        [Required(ErrorMessage = "Division is required")]
        [ForeignKey(nameof(Division))]
        public int DivisionId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Division Division { get; set; }
    }
}