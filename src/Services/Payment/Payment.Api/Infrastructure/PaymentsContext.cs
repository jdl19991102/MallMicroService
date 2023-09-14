using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Payment.Api.Models;

namespace Payment.Api.Infrastructure
{
    public partial class PaymentsContext : DbContext
    {
        public PaymentsContext()
        {
        }

        public PaymentsContext(DbContextOptions<PaymentsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PaymentInfo> PaymentInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Payments;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentInfo>(entity =>
            {
                entity.ToTable("PaymentInfo");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.PaymentAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasMaxLength(50);

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'微信支付')")
                    .HasComment("支付方式：支付宝、微信、PayPal");

                entity.Property(e => e.PaymentStatus).HasComment("0 待支付，1 已支付，2 支付失败");

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
