using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data.Domain.Entities
{
    public interface IEntityAuditable
    {
        /* auditable fields */
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
