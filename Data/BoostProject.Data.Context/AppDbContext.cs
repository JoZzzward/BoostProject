using BoostProject.Common.Enums;
using BoostProject.Data.Entities.AppUsers;
using BoostProject.Data.Entities.Feedbacks;
using BoostProject.Data.Entities.GameAccounts;
using BoostProject.Data.Entities.Messages;
using BoostProject.Data.Entities.Orders;
using Data.Entities.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoostProject.Data.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<GameAccount> GameAccounts { get; set; }

    public override DbSet<AppUser> Users { get; set; }
    public override DbSet<AppRole> Roles { get; set; }
    public new DbSet<AppUserRole> UserRoles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
		: base (options) => AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Initializing User, UserRoles and etc. models identity tables names
        builder.Entity<AppUser>().ToTable("Users");
        builder.Entity<AppRole>().ToTable("UserRoles");
        builder.Entity<AppRole>().HasKey(x => x.Id);
        builder.Entity<AppUserRole>().ToTable("UsersRoleOwners");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UsersTokens");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("UserRoleClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UsersLogins");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UsersClaims");

        // Initializing User model
        builder.Entity<AppUser>().Property(x => x.UserName).IsRequired();
        builder.Entity<AppUser>().Property(x => x.PhoneNumber).HasMaxLength(12);
        builder.Entity<AppUser>().HasIndex(x => x.PhoneNumber).IsUnique();
        builder.Entity<AppUser>().HasIndex(x => x.Email).IsUnique();
        builder.Entity<AppUser>().Property(x => x.Email).HasMaxLength(50);
        builder.Entity<AppUser>().Property(x => x.CreationDateTime).HasDefaultValue(DateTime.Now);

        builder.Entity<AppUser>().Property(x => x.FirstName).HasDefaultValue(string.Empty);
        builder.Entity<AppUser>().Property(x => x.LastName).HasDefaultValue(string.Empty);
        builder.Entity<AppUser>().Property(x => x.SecondName).HasDefaultValue(string.Empty);
        builder.Entity<AppUser>().Property(x => x.ImageUri).HasDefaultValue(string.Empty);
        // TODO: Change ImageUri to actual path to photos

        // Initializing Message model
        builder.Entity<Message>().ToTable("Messages");
        builder.Entity<Message>().Property(x => x.Seen).HasDefaultValue(SeenStatus.NotSeen);
        builder.Entity<Message>().Property(x => x.Content).IsRequired();
        builder.Entity<Message>().Property(x => x.Content).HasMaxLength(250);

        // Initializing Feedbacks model
        builder.Entity<Feedback>().ToTable("Feedbacks");
        builder.Entity<Feedback>().Property(x => x.Content).IsRequired();
        builder.Entity<Feedback>().Property(x => x.Content).HasMaxLength(250);

        // Initializing Orders model
        builder.Entity<Order>().ToTable("Orders");
        builder.Entity<Order>().Property(x => x.Game).IsRequired();
        builder.Entity<Order>().Property(x => x.CustomerId).IsRequired();
        builder.Entity<Order>().Property(x => x.Cost).HasDefaultValue(0);
        builder.Entity<Order>().Property(x => x.Status).HasDefaultValue(OrderStatus.NoActive);

        // Initializing GameAccounts model
        builder.Entity<GameAccount>().ToTable("GameAccounts");
        builder.Entity<GameAccount>().Property(x => x.Label).HasMaxLength(100);
        builder.Entity<GameAccount>().Property(x => x.GameHours).IsRequired();
        builder.Entity<GameAccount>().Property(x => x.Cost).IsRequired();

        // Foreign keys

        builder.Entity<AppUserRole>()
            .HasOne(x => x.User)
            .WithMany(x => x.Roles)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AppUserRole>()
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AppUser>()
            .HasMany(x => x.Roles)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Feedback>()
            .HasOne(m => m.User)
            .WithMany(u => u.Feedbacks)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<GameAccount>()
            .HasOne(m => m.Seller)
            .WithMany(u => u.GameAccounts)
            .HasForeignKey(m => m.SellerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .HasOne(m => m.Customer)
            .WithMany(u => u.Orders)
            .HasForeignKey(m => m.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .HasOne(m => m.Booster)
            .WithMany()
            .HasForeignKey(m => m.BoosterId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
