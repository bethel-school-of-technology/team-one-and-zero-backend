using Microsoft.EntityFrameworkCore;
using TEAM_ONE_AND_ZERO_BACKEND.Models;

namespace TEAM_ONE_AND_ZERO_BACKEND.Migrations;

public class PoPDbContext : DbContext
{
    public DbSet<Comment> Comment {get; set;}
    public DbSet<User> Users {get; set;}
    public PoPDbContext(DbContextOptions<PoPDbContext> options)
        : base(options)
        {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Comment>(entity => {
            entity.HasKey(e => e.CommentID);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Username).IsRequired();
            entity.HasIndex(e => e.SongId);
            entity.Property(e => e.SongId).IsRequired();
            entity.Property(e => e.CreatedAt);
            entity.HasIndex(e => e.UserId);
            entity.Property(e => e.UserId).IsRequired();
        });
        
        modelBuilder.Entity<User>(entity => {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Username).IsRequired();
            entity.HasIndex(x => x.Username).IsUnique(); //making sure each user has their own unique username
            entity.Property(e => e.Email).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique(); //making sure each user has a unique email
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Birthdate);
            entity.Property(e => e.ProfilePhoto).IsRequired();

        });
    }
}
