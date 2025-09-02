using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceManager.Auth.Models
{
    /// <summary>
    /// 25.08.2025
    /// </summary>
    public class AdminUserUnitAccess
    {
        public int Id { get; set; }
        public string UserRowGuid { get; set; }
        public string UnitCode { get; set; }
    }
}
