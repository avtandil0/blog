using System;
using System.Collections.Generic;
using System.Text;
using blog.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;


namespace blog.DAL.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
