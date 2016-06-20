using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Tracker.Blog.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}