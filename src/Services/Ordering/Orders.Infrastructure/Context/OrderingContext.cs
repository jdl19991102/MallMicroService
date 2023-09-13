using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Orders.Domain.Models;

namespace Orders.Infrastructure.Context
{
    public partial class OrderingContext : DbContext
    {
        public OrderingContext()
        {
        }

        public OrderingContext(DbContextOptions<OrderingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OrdersDetail> OrdersDetails { get; set; } = null!;
        public virtual DbSet<OrdersInfo> OrdersInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Ordering;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdersDetail>(entity =>
            {
                entity.ToTable("Orders_Detail");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DetailsPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Details_Price")
                    .HasComment("子订单价格");

                entity.Property(e => e.OrdersId).HasColumnName("Orders_Id");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.ProductQuantity)
                    .HasColumnName("Product_Quantity")
                    .HasComment("商品数量");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrdersInfo>(entity =>
            {
                entity.ToTable("OrdersInfo");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.OrderName).HasMaxLength(50);

                entity.Property(e => e.OrderUniqueId).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        // 重写SaveChanges方法
        public override int SaveChanges()
        {
            // 获取所有变化的实体
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                // 如果是新增
                if (entry.State == EntityState.Added)
                {
                    // 设置创建时间
                    entry.Property("CreateTime").CurrentValue = DateTime.Now;
                    // 设置更新时间
                    entry.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
                // 如果是修改
                if (entry.State == EntityState.Modified)
                {
                    // 设置更新时间
                    entry.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentTime = DateTime.Now;

            var changedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in changedEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    // 设置创建时间
                    entry.Property("CreateTime").CurrentValue = currentTime;
                }
                // 设置更新时间
                Entry(entry.Entity).Property("UpdateTime").CurrentValue = currentTime;
                //entity.Property("UpdateTime").CurrentValue = currentTime; // 两种写法都可以
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
