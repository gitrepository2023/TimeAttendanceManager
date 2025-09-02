using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceManager.Features.Masters.Departments.Models
{
    /// <summary>
    /// 29.08.2025
    /// </summary>
    public class Department
    {
        public int? Id { get; set; }
        public string UnitCode { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool? IsActive { get; set; }
        public int? RowVersion { get; set; }
    }
}
