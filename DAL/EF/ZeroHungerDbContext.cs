using System;
using System.Collections.Generic;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public partial class ZeroHungerDbContext : DbContext
{
    public ZeroHungerDbContext()
    {
    }

    public ZeroHungerDbContext(DbContextOptions<ZeroHungerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CollectRequest> CollectRequests { get; set; }

    public virtual DbSet<DistributionRecord> DistributionRecords { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<FoodItem> FoodItems { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<StatusLog> StatusLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DbConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CollectRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CollectR__3214EC07E06AFB77");

            entity.Property(e => e.AcceptedAt).HasColumnType("datetime");
            entity.Property(e => e.CollectedAt).HasColumnType("datetime");
            entity.Property(e => e.CompletedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DistributedAt).HasColumnType("datetime");
            entity.Property(e => e.MaxPreserveUntil).HasColumnType("datetime");
            entity.Property(e => e.PickupNotes)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("PENDING");

            entity.HasOne(d => d.Employee).WithMany(p => p.CollectRequests)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_CollectRequests_Employee");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.CollectRequests)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CollectRequests_Restaurant");
        });

        modelBuilder.Entity<DistributionRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Distribu__3214EC0705BCF7E1");

            entity.HasIndex(e => e.CollectRequestId, "UQ_DistributionRecords_CollectRequestId").IsUnique();

            entity.Property(e => e.DistributedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DistributionPoint)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.CollectRequest).WithOne(p => p.DistributionRecord)
                .HasForeignKey<DistributionRecord>(d => d.CollectRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DistributionRecords_CollectRequest");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07CE920639");

            entity.HasIndex(e => e.UserId, "UQ_Employees_UserId").IsUnique();

            entity.Property(e => e.Availability)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("AVAILABLE");
            entity.Property(e => e.CoverageArea)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Users");
        });

        modelBuilder.Entity<FoodItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodItem__3214EC07F61BFF6F");

            entity.Property(e => e.Category)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("COOKED");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.CollectRequest).WithMany(p => p.FoodItems)
                .HasForeignKey(d => d.CollectRequestId)
                .HasConstraintName("FK_FoodItems_CollectRequest");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restaura__3214EC07F032ABC3");

            entity.HasIndex(e => e.UserId, "UQ_Restaurants_UserId").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithOne(p => p.Restaurant)
                .HasForeignKey<Restaurant>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Restaurants_Users");
        });

        modelBuilder.Entity<StatusLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatusLo__3214EC07D864646C");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NewStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Note)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.OldStatus)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.StatusLogs)
                .HasForeignKey(d => d.ChangedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatusLogs_ChangedBy");

            entity.HasOne(d => d.CollectRequest).WithMany(p => p.StatusLogs)
                .HasForeignKey(d => d.CollectRequestId)
                .HasConstraintName("FK_StatusLogs_CollectRequest");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0703236E3B");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(180)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
