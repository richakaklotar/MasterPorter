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
    public partial class JobCard
    {
        [Key]
        public int JobCardID { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Operator is required")]
        public string Operator { get; set; }

        [Required(ErrorMessage = "Shift is required")]
        [ForeignKey(nameof(Shift))]
        public int ShiftID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Shift Shift { get; set; }

        [Required(ErrorMessage = "Project is required")]
        [ForeignKey(nameof(Project))]
        public int ProjectID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Project Project { get; set; }

        [Required(ErrorMessage = "Component is required")]
        [ForeignKey(nameof(Components))]
        public int ComponentID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Components Components { get; set; }

        [Required(ErrorMessage = "Activity is required")]
        [ForeignKey(nameof(Activities))]
        public int ActivitiesID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Activities Activities { get; set; }

        [Required(ErrorMessage = "Sub Activity is required")]
        [ForeignKey(nameof(SubActivities))]
        public int SubActivitiesID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual SubActivities SubActivities { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public TimeOnly EndTime { get; set; }

        [Required(ErrorMessage = "Total Hours is required")]
        [Range(0, 24, ErrorMessage = "Total Hours must be between 0 and 24")]
        public decimal TotalHours { get; set; }

        [StringLength(100, ErrorMessage = "Remarks cannot exceed 100 characters")]
        public string Remarks { get; set; }

        public string IsRework { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}
