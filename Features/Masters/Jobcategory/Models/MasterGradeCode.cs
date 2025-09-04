using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceManager.Features.Masters.Jobcategory.Models
{
    public class MasterGradeCode
    {
        public int? Id { get; set; }
        public string UnitCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? CodeLevel { get; set; }
        public bool? IsActive { get; set; }
        public int? RowVersion { get; set; }
    }
}
