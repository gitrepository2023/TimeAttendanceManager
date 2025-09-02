using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceManager.Auth.Models
{
    /// <summary>
    /// 21.08.2025
    /// </summary>
    public class AdminMenuUserPerm
    {
        public int Id { get; set; }
        public string UserRowGuid { get; set; }
        public int? MenuId { get; set; }
        public bool? CanView { get; set; }
        public DateTime? GrantedAt { get; set; }
        public string GrantedBy { get; set; }
    }
}
