using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntertnshipTest.DAL
{
    public class InternshipContext : DbContext
    {
        public InternshipContext(DbContextOptions<InternshipContext> options) 
            : base(options) 
        {

        }
    }
}
