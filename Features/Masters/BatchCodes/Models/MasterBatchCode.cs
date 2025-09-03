using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceManager.Features.Masters.BatchCodes.Models
{
    /// <summary>
    /// 03.09.2025
    /// </summary>
    public class MasterBatchCode
    {
        public int? Id { get; set; }
        public string UnitCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int RowVersion { get; set; }
    }
}
