using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppTest.Models
{
    public class ItemContext : DbContext
    {
        public ItemContext (DbContextOptions<ItemContext> options) : base(options)
        {

        }
        public DbSet<TestItem> TestItems { get; set; }

    }
}
