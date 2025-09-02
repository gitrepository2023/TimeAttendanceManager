using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceManager.Main.Classes
{
    public class SearchOptions
    {
        public bool MatchCase { get; set; }
        public bool WholeWord { get; set; }
        public bool SearchInTags { get; set; }
    }
}
