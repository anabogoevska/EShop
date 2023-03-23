using EShop.Web.Models.Domain;
using EShop.Web.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<EShopApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //gi kreirame DbSet

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Id na Product da se generira koga ke dodademe nov zapis, da se generira novo Id
            builder.Entity<Product>()
               .Property(z => z.Id)
               .ValueGeneratedOnAdd();

            //Id na ShoppingCart da se generira koga ke dodademe nov zapis, da se generira novo Id
            builder.Entity<ShoppingCart>()
               .Property(z => z.Id)
               .ValueGeneratedOnAdd();

            //primaren kluc na ManyToMany relacijata e kompoziten primaren kluc od ProductId i ShoppingCartId
            builder.Entity<ProductInShoppingCart>()
                .HasKey(z => new { z.ProductId, z.ShoppingCartId });

            //Za ManyToMany relacijata
            builder.Entity<ProductInShoppingCart>()
                .HasOne(z => z.Product)
                .WithMany(z => z.ProductInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<ProductInShoppingCart>()
               .HasOne(z => z.ShoppingCart)
               .WithMany(z => z.ProductInShoppingCarts)
               .HasForeignKey(z => z.ProductId);

            builder.Entity<ShoppingCart>()
                .HasOne<EShopApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);
        }
    }
}
