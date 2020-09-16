using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class EmployeeVm
    {
        
            public string EmployeeId { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }

            public DateTimeOffset CreateDate { get; set; }
            public DateTimeOffset UpdateDate { get; set; }
            public DateTimeOffset DeleteData { get; set; }
            public bool isDelete { get; set; }
        
    }
}
