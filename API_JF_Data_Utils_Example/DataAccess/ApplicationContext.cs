using JF.Utils.Data;
using Microsoft.EntityFrameworkCore;

namespace API_JF_Data_Utils_Example.DataAccess
{
    public class ApplicationContext : JFContext
    {
        public ApplicationContext(DbContextOptions<JFContext> options) : base(options)
        {
        }

    }
}
