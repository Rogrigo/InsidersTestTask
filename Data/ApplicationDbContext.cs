﻿using InsidersTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace InsidersTestTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<CryptoInfo> CryptoInfos { get; set; }
        public DbSet<UserCrypto> UserCryptos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCrypto>().HasKey(uc => uc.Id);

            modelBuilder.Entity<UserCrypto>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCryptos)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCrypto>()
                .HasOne(uc => uc.CryptoInfo)
                .WithMany(ci => ci.UserCryptos)
                .HasForeignKey(uc => uc.CryptoId);
        }
    }
}
