﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;

#nullable disable

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<int>("AppliedPromocodesCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers", t =>
                        {
                            t.HasComment("Customer domain model.");
                        });
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PreferenceId")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId", "PreferenceId");

                    b.HasIndex("PreferenceId");

                    b.ToTable("CustomerPreference");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PartnerManagerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PartnerName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PreferenceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceInfo")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PartnerManagerId");

                    b.HasIndex("PreferenceId");

                    b.ToTable("PromoCodes");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", "Customer")
                        .WithMany("Preferences")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", "Preference")
                        .WithMany()
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", "Customer")
                        .WithMany("PromoCodes")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Employee", "PartnerManager")
                        .WithMany()
                        .HasForeignKey("PartnerManagerId");

                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", "Preference")
                        .WithMany()
                        .HasForeignKey("PreferenceId");

                    b.Navigation("Customer");

                    b.Navigation("PartnerManager");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.Navigation("Preferences");

                    b.Navigation("PromoCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
