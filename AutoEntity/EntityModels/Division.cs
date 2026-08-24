using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    public partial class Division
    {
        [Key]
        public int DivisionId { get; set; }
        [Required]
        public string DivisionName { get; set; }
        [Required]
        public string DivisionCode { get; set; }

        [ForeignKey(nameof(Plant))]
        public int PlantId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Plant Plant { get; set; }
    }
}
