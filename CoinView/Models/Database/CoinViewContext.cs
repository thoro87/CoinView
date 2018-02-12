using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoinView.Models.Database
{
    public partial class CoinViewContext : DbContext
    {
        public virtual DbSet<Buy> Buys { get; set; }
        public virtual DbSet<Coin> Coins { get; set; }
        public virtual DbSet<CoinValue> CoinValues { get; set; }
        public virtual DbSet<Creation> Creations { get; set; }
        public virtual DbSet<Trade> Trades { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }

        public CoinViewContext(DbContextOptions<CoinViewContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buy>(entity =>
            {
                entity.Property(e => e.BuyId).HasColumnName("BuyID");

                entity.Property(e => e.AmountBought).HasColumnType("decimal(16, 8)");

                entity.Property(e => e.AmountInWallet).HasColumnType("decimal(16, 8)");

                entity.Property(e => e.CoinId).HasColumnName("CoinID");

                entity.Property(e => e.ExchangeWalletId).HasColumnName("ExchangeWalletID");

                entity.Property(e => e.PriceEur)
                    .HasColumnName("PriceEUR")
                    .HasColumnType("decimal(16, 8)");

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasMaxLength(63);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WalletId).HasColumnName("WalletID");

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.Buys)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkBuysCoinID");

                entity.HasOne(d => d.ExchangeWallet)
                    .WithMany(p => p.BuyExchangeWallets)
                    .HasForeignKey(d => d.ExchangeWalletId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkBuysExchangeWalletID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Buys)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkBuysUserID");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.BuyWallets)
                    .HasForeignKey(d => d.WalletId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkBuysWalletID");
            });

            modelBuilder.Entity<Coin>(entity =>
            {
                entity.Property(e => e.CoinId).HasColumnName("CoinID");

                entity.Property(e => e.CoinMarketCapId)
                    .IsRequired()
                    .HasColumnName("CoinMarketCapID")
                    .HasMaxLength(63);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(63);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(63);
            });

            modelBuilder.Entity<CoinValue>(entity =>
            {
                entity.Property(e => e.CoinValueId).HasColumnName("CoinValueID");

                entity.Property(e => e.CoinId).HasColumnName("CoinID");

                entity.Property(e => e.PercentChange1h).HasColumnType("decimal(16, 8)");

                entity.Property(e => e.PercentChange24h).HasColumnType("decimal(16, 8)");

                entity.Property(e => e.PercentChange7d).HasColumnType("decimal(16, 8)");

                entity.Property(e => e.PriceBtc)
                    .HasColumnName("PriceBTC")
                    .HasColumnType("decimal(16, 8)");

                entity.Property(e => e.PriceEur)
                    .HasColumnName("PriceEUR")
                    .HasColumnType("decimal(16, 8)");

                entity.Property(e => e.PriceUsd)
                    .HasColumnName("PriceUSD")
                    .HasColumnType("decimal(16, 8)");

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.CoinValues)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCoinValuesCoinID");
            });

            modelBuilder.Entity<Creation>(entity =>
            {
                entity.Property(e => e.CreationId).HasColumnName("CreationID");

                entity.Property(e => e.Amount).HasColumnType("decimal(16, 8)");

                entity.Property(e => e.CoinId).HasColumnName("CoinID");

                entity.Property(e => e.SellPriceBtc)
                    .HasColumnName("SellPriceBTC")
                    .HasColumnType("decimal(16, 8)");

                entity.Property(e => e.SellPricePerShare).HasColumnType("decimal(16, 8)");

                entity.Property(e => e.SellWalletId).HasColumnName("SellWalletID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WalletId).HasColumnName("WalletID");

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.Creations)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCreationsCoinID");

                entity.HasOne(d => d.SellWallet)
                    .WithMany(p => p.CreationSellWallets)
                    .HasForeignKey(d => d.SellWalletId)
                    .HasConstraintName("fkCreationsSellWalletID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Creations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCreationsUserID");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.CreationWallets)
                    .HasForeignKey(d => d.WalletId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCreationsWalletID");
            });

            modelBuilder.Entity<Trade>(entity =>
            {
                entity.Property(e => e.TradeId).HasColumnName("TradeID");

                entity.Property(e => e.Amount).HasColumnType("decimal(16, 8)");

                entity.Property(e => e.BuyPriceBtc)
                    .HasColumnName("BuyPriceBTC")
                    .HasColumnType("decimal(16, 8)");

                entity.Property(e => e.BuyPricePerShare).HasColumnType("decimal(16, 8)");

                entity.Property(e => e.BuyWalletId).HasColumnName("BuyWalletID");

                entity.Property(e => e.CoinId).HasColumnName("CoinID");

                entity.Property(e => e.SellPriceBtc)
                    .HasColumnName("SellPriceBTC")
                    .HasColumnType("decimal(16, 8)");

                entity.Property(e => e.SellPricePerShare).HasColumnType("decimal(16, 8)");

                entity.Property(e => e.SellWalletId).HasColumnName("SellWalletID");

                entity.Property(e => e.StoreWalletId).HasColumnName("StoreWalletID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.BuyWallet)
                    .WithMany(p => p.TradeBuyWallets)
                    .HasForeignKey(d => d.BuyWalletId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkTradesBuyWalletID");

                entity.HasOne(d => d.Coin)
                    .WithMany(p => p.Trades)
                    .HasForeignKey(d => d.CoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkTradesCoinID");

                entity.HasOne(d => d.SellWallet)
                    .WithMany(p => p.TradeSellWallets)
                    .HasForeignKey(d => d.SellWalletId)
                    .HasConstraintName("fkTradesSellWalletID");

                entity.HasOne(d => d.StoreWallet)
                    .WithMany(p => p.TradeStoreWallets)
                    .HasForeignKey(d => d.StoreWalletId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkTradesStoreWalletID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Trades)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkTradesUserID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(63);
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(e => e.WalletId).HasColumnName("WalletID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(63);
            });
        }
    }
}
