using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        [StringLength(20)]
        [Required]
        public string Surname { get; set; }

        [StringLength(100)]
        [Required]
        public string Address { get; set; }

        [Required]
        public string Qualification { get; set; }

        [RegularExpression("([1-9]{1}[0-9]{9})", ErrorMessage = "Enter only numeric number")]
        [Required]
        public long Contact_Number { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public Department department { get; set; }
    }
}
