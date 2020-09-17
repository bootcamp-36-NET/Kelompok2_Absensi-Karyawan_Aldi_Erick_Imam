using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Absence")]
    public class Absence
    {
        public string Id { get; set; }
        [ForeignKey("tb_m_user")]
        public string UserId { get; set; }
        public DateTimeOffset TimeIn { get; set; }
        public DateTimeOffset TimeOut { get; set; }

        public virtual User User { get; set; }
    }
}
