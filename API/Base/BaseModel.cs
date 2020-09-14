using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Base
{
    public interface BaseModel
    {
        int Id { set; get; }
        string Name { set; get; }
        DateTimeOffset createdDate { set; get; }
        DateTimeOffset deletedDate { set; get; }
        DateTimeOffset updatedDate { set; get; }
        bool isDelete { set; get; }
    }
}
