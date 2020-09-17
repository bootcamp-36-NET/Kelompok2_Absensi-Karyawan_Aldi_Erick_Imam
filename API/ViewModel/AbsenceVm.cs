using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class AbsenceVm
    {
        public string Id {get; set;}
        public DateTimeOffset TimeIn { get; set; }
        public DateTimeOffset TimeOut { get; set; }
        public string UserId { get; set; }

        public string Type { get; set; }
    }
}
