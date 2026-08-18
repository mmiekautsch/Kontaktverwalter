using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kontaktverwalter.API.DBModel;

public partial class ContactManagerDbContext : DbContext
{
    public ContactManagerDbContext(DbContextOptions<ContactManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PhoneContact> PhoneContacts { get; set; }

    public virtual DbSet<ViewFullContactInfo> ViewFullContactInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.IdAddress);

            entity.ToTable("Address");

            entity.Property(e => e.IdAddress).HasColumnName("ID_Address");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.FkPerson).HasColumnName("FK_Person");
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.StreetName).HasMaxLength(100);
            entity.Property(e => e.StreetNumber).HasMaxLength(10);

            entity.HasOne(d => d.FkPersonNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.FkPerson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_Person");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.IdPerson);

            entity.ToTable("Person");

            entity.Property(e => e.IdPerson).HasColumnName("ID_Person");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LastNameUpperCase).HasMaxLength(100);
        });

        modelBuilder.Entity<PhoneContact>(entity =>
        {
            entity.HasKey(e => e.IdPhoneContact);

            entity.ToTable("PhoneContact");

            entity.Property(e => e.IdPhoneContact).HasColumnName("ID_PhoneContact");
            entity.Property(e => e.FkPerson).HasColumnName("FK_Person");
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(30);

            entity.HasOne(d => d.FkPersonNavigation).WithMany(p => p.PhoneContacts)
                .HasForeignKey(d => d.FkPerson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhoneContact_Person");
        });

        modelBuilder.Entity<ViewFullContactInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewFullContactInfo");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IdPerson).HasColumnName("ID_Person");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.StreetName).HasMaxLength(100);
            entity.Property(e => e.StreetNumber).HasMaxLength(10);
            entity.Property(e => e.Type).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
