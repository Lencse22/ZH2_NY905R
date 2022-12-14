using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZH2_NY905R.Moduls;

public partial class OrderContext : DbContext
{
    public OrderContext()
    {
    }

    public OrderContext(DbContextOptions<OrderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ny905r\\source\\repos\\ZH2_NY905R\\ZH2_NY905R\\OrdersDatabase.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerPk).HasName("PK__Customer__A4AFAF880A6C58FD");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerPk).HasColumnName("CustomerPK");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderPk).HasName("PK__Order__C390638A0C2883F3");

            entity.ToTable("Order");

            entity.Property(e => e.OrderPk).HasColumnName("OrderPK");
            entity.Property(e => e.CustomerFk).HasColumnName("CustomerFK");
            entity.Property(e => e.ProductFk).HasColumnName("ProductFK");

            entity.HasOne(d => d.CustomerFkNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__CustomerF__3B75D760");

            entity.HasOne(d => d.ProductFkNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__ProductFK__3C69FB99");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductPk).HasName("PK__Product__B40D0B93D1280AA2");

            entity.ToTable("Product");

            entity.Property(e => e.ProductPk).HasColumnName("ProductPK");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UnitName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
