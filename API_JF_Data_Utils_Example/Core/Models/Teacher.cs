using JF.Utils.Data;
using JF.Utils.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_JF_Data_Utils_Example.Core.Models
{
    public class Teacher : Person
    {
        public virtual ICollection<Salon> Salons { get; set; }
    }
}
