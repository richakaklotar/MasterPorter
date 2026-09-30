using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoEntity.EntityModels
{
    [Index(nameof(PlantName), IsUnique = true)]
    [Index(nameof(PlantCode), IsUnique = true)]
    public partial class Plant
    {
        public int PlantId { get; set; }
        [Required(ErrorMessage = "Plant Name is required")]
        [StringLength(25, ErrorMessage = "Plant Name cannot exceed 25 characters.")]
        public string PlantName { get; set; }
        [Required(ErrorMessage = "Plant Code is required")]
        [StringLength(25, ErrorMessage = "Plant Code cannot exceed 25 characters.")]
        public string PlantCode { get; set; }
        public string Status { get; set; }
    }
}
