using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollaborativeWhiteboard.Models
{
    public class AppDBContext: IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppDBContext(DbContextOptions options): base(options)
        {
            _options = options;
        }

        public DbSet<UserWhiteBoard> UserWhiteBoard { get; set; }
        public DbSet<Whiteboard> Whiteboard { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserWhiteBoard>().HasKey(x => new { x.UserId, x.WhiteboardId });
            base.OnModelCreating(builder);
        }
    }
}
