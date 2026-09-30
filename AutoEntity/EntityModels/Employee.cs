using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoEntity.EntityModels
{
    [Index(nameof(EmployeeCode), IsUnique = true)]
    [Index(nameof(EmployeeName), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public partial class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        [StringLength(25, ErrorMessage = "Employee Name cannot exceed 25 characters")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Employee Code is required")]
        [StringLength(25, ErrorMessage = "Employee Code cannot exceed 25 characters")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Joining Date is required")]
        [Column(TypeName = "date")]
        [NotMapped]
        public DateTime JoiningDate { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        [ForeignKey(nameof(Designation))]
        public int DesignationID { get; set; }

        [Required(ErrorMessage = "Shift is required")]
        [ForeignKey(nameof(Shift))]
        public int ShiftID { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Designation Designation { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public virtual Shift Shift { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}