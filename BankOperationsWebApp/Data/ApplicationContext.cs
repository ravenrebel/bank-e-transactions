using BankOperationsWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankOperationsWebApp.Data
{
    public sealed class ApplicationContext:IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Card> Cards {  get; set;  }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Card>( b =>
               {
                   b.HasKey(c => c.Id);

                   b.HasIndex(c => c.CardNumber)
                   .IsUnique();

                   b.Property<string>(u => u.PinCode)
                .IsRequired();
               }
            );
            builder.Entity<User>(b =>
            {
                b.HasOne<Card>(u => u.Card)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

                b.Property<int>(u => u.CardId)
                .IsRequired();
            });
        }
    }
}