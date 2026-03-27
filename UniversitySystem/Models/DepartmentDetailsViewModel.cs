using System.Collections.Generic;
using UniversitySystem.Models;

namespace UniversitySystemMVC.Models
{
    public class DepartmentDetailsViewModel
    {
        public string DepartmentName { get; set; }
        public string DepartmentState { get; set; } // Main لو >50 , Branch لو <50
        public List<Student> StudentsOver25 { get; set; } = new List<Student>();
    }
}