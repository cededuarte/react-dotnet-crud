using System;
using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Data
{
    internal sealed class AppDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        //TO USE DBSQLITE
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) => dbContextOptionsBuilder.UseSqlite("Data Source =./Data/AppDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)

         //SEEDING FIRST MIGRATION TO SQLITE --DUMMY DATA
        {
            Post[] postsToSeed = new Post[6];

            for(int i = 1; i<= 6; i++)
            {
                postsToSeed[i - 1] = new Post
                {
                    PostId = i,
                    Name = $"Post {i}",
                    Email = $"database{i}@email.com",
                    Contact = $"000-000{i}"
                };
            }
            //ENTIITY TO SEED
            modelBuilder.Entity<Post>().HasData(postsToSeed);
        }

    }
}

