using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LiseWoreEmp.Models;

public partial class LiseWoreEmpContext : DbContext
{
    public LiseWoreEmpContext()
    {
    }

    public LiseWoreEmpContext(DbContextOptions<LiseWoreEmpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<UserList> UserLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=dbConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11FA5FDF3E");

            entity.HasIndex(e => e.Phone, "UQ__Employee__5C7E359EAEDDE8A7").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534991AC56E").IsUnique();

            entity.Property(e => e.Department).HasMaxLength(25);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(25);
            entity.Property(e => e.LastName).HasMaxLength(25);
            entity.Property(e => e.MiddleName).HasMaxLength(25);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Position).HasMaxLength(25);

            entity.HasOne(d => d.AddedByNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AddedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Added__3C69FB99");
        });

        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserList__1788CC4CB6A6DC04");

            entity.ToTable("UserList");

            entity.HasIndex(e => e.EmailAddress, "UQ__UserList__49A147407A39ACDA").IsUnique();

            entity.Property(e => e.EmailAddress).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
