using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Models;

namespace PetShop.Data
{
    public class PetShopContext : DbContext
    {
        public PetShopContext (DbContextOptions<PetShopContext> options)
            : base(options)
        {
        }

        public DbSet<PetShop.Data.Pet> Pet { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
