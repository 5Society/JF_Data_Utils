using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data.Domain.Entities
{
    public class EntityKey : IEntityKey
    {
        [Key]
        public int Id { get; set; }
    }
}
