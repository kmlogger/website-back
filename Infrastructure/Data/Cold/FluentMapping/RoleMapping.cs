using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.FluentMapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(r => r.Id);

            // Propriedades
            builder.Property(r => r.Id)
                .HasColumnName("Id")
                .HasColumnType("UUID")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(r => r.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("DateTime")
                .HasDefaultValueSql("now()");

            builder.Property(r => r.UpdatedDate)
                .HasColumnName("UpdatedDate")
                .HasColumnType("DateTime")
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder.Property(r => r.DeletedDate)
                .HasColumnName("DeletedDate")
                .HasColumnType("DateTime")
                .IsRequired(false);

            builder.OwnsOne(r => r.Name, name =>
            {
                name.Property(n => n.Name)
                    .HasColumnName("Name")
                    .HasColumnType("String")
                    .HasMaxLength(100);
            });

            builder.Property(r => r.Slug)
                .HasColumnName("Slug")
                .HasColumnType("String");

            builder
                .HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    userRole => userRole
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    userRole => userRole
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
