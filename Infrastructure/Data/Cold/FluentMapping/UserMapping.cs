using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Cold.FluentMapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            // Propriedades
            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasColumnType("UUID")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("DateTime")
                .HasDefaultValueSql("now()");

            builder.Property(c => c.UpdatedDate)
                .HasColumnName("UpdatedDate")
                .HasColumnType("DateTime")
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder.Property(c => c.DeletedDate)
                .HasColumnName("DeletedDate")
                .HasColumnType("DateTime")
                .IsRequired(false);

            builder.OwnsOne(u => u.FullName, fullName =>
            {
                fullName.Property(f => f.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("FirstName")
                    .HasColumnType("String");

                fullName.Property(f => f.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("LastName")
                    .HasColumnType("String");
            });

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Address)
                    .HasColumnName("Email")
                    .HasMaxLength(50)
                    .HasColumnType("String");
            });

            builder.OwnsOne(u => u.Address, address =>
            {
                address.Property(a => a.Road)
                    .HasColumnName("Road")
                    .HasMaxLength(100)
                    .IsRequired(false)
                    .HasColumnType("String");

                    address.Property(a => a.Number)
                        .HasColumnName("Number")
                        .HasColumnType("Int64")
                        .IsRequired(false); 

                address.Property(a => a.NeighBordHood)
                    .HasColumnName("NeighborHood")
                    .IsRequired(false)
                    .HasColumnType("String");

                address.Property(a => a.Complement)
                    .HasColumnName("Complement")
                    .HasMaxLength(100)
                    .IsRequired(false)
                    .HasColumnType("String");
            });

            builder.Property(u => u.TokenActivate)
                .HasColumnName("TokenActivate")
                .IsRequired(false)
                .HasColumnType("String");

            builder.Property(u => u.Active)
                .HasColumnName("Active")
                .HasColumnType("UInt8");

            builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.Hash)
                    .HasColumnName("Hash")
                    .IsRequired(false)
                    .HasColumnType("String");

                password.Property(p => p.Salt)
                    .HasColumnName("Salt")
                    .IsRequired(false)
                    .HasColumnType("String");

                password.Ignore(p => p.Content);
            });

            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    role => role
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    user => user
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
