using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AutoEntity.EntityModels
{
    [Index(nameof(DivisionName), IsUnique = true)]
    [Index(nameof(DivisionCode), IsUnique = true)]
    public partial class Division
    {
        [Key]
        public int DivisionId { get; set; }
        [Required(ErrorMessage = "Division Name is required")]
        [StringLength(25, ErrorMessage = "Division Name cannot exceed 25 characters")]
        public string DivisionName { get; set; }
        [Required(ErrorMessage = "Division Code is required")]
        [StringLength(25, ErrorMessage = "Division Code cannot exceed 25 characters")]
        public string DivisionCode { get; set; }

        [ForeignKey(nameof(Plant))]
        public int PlantId { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Plant Plant { get; set; }
    }
}
