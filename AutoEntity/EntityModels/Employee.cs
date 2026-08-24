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
    public partial class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateOnly JoiningDate { get; set; }
        [ForeignKey(nameof(Designation))]
        public int DesignationID { get; set; }
        [ForeignKey(nameof(Shift))]
        public int ShiftID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Designation Designation { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public virtual Shift Shift { get; set; }
    }
}
