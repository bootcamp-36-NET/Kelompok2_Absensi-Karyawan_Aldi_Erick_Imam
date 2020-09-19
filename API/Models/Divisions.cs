using API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_division")]
    public class Divisions : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset createdDate { get; set; }
        public DateTimeOffset deletedDate { get; set; }
        public DateTimeOffset updatedDate { get; set; }
        public bool isDelete { get; set; }
        [ForeignKey("DepartmentId")]
        public int DepartmentId { set; get; }
        public Department Department { set; get; }
    }
}
