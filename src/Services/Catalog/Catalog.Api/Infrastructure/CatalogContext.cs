using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Catalog.Api.Models;

namespace Catalog.Api.Infrastructure
{
    public partial class CatalogContext : DbContext
    {
        public CatalogContext()
        {
        }

        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CatalogItem> CatalogItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogItem>(entity =>
            {
                entity.ToTable("CatalogItem");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("商品名字");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasComment("商品价格");

                entity.Property(e => e.Stock).HasComment("库存数量");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


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
