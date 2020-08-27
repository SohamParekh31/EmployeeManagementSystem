using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [DisplayName("Department Name")]
        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
