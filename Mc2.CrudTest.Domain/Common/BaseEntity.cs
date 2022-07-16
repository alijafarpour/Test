using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; } // Used For Soft delete
    }
}
