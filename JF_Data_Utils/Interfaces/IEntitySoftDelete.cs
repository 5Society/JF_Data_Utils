using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data.Interfaces
{
    internal interface IEntitySoftDelete
    {
        public DateTime? DeletedDate { get; set; }

        public string? DeletedBy { get; set; }

        public bool IsDeleted { get => DeletedBy != null; }
    }
}
