﻿// <auto-generated />
using System;
using ApiFortress.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIFortress.Infrastructure.Migrations
{
    [DbContext(typeof(ApiFortressDbContext))]
    partial class ApiFortressDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIUserRole", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("APIUserRole");

                    b.HasData(
                        new
                        {
                            RolesId = 1,
                            UsersId = 1
                        },
                        new
                        {
                            RolesId = 2,
                            UsersId = 2
                        },
                        new
                        {
                            RolesId = 2,
                            UsersId = 3
                        },
                        new
                        {
                            RolesId = 2,
                            UsersId = 4
                        },
                        new
                        {
                            RolesId = 2,
                            UsersId = 5
                        });
                });

            modelBuilder.Entity("ApiFortress.Domain.Entities.APIUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PublicKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PublicKey = "AdminKey_ABC123",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            PublicKey = "User1Key_DEF456",
                            Username = "user1"
                        },
                        new
                        {
                            Id = 3,
                            PublicKey = "User2Key_GHI789",
                            Username = "user2"
                        },
                        new
                        {
                            Id = 4,
                            PublicKey = "User3Key_JKL012",
                            Username = "user3"
                        },
                        new
                        {
                            Id = 5,
                            PublicKey = "User4Key_MNO345",
                            Username = "user4"
                        });
                });

            modelBuilder.Entity("ApiFortress.Domain.Entities.AuditDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EventDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AuditDetails");
                });

            modelBuilder.Entity("ApiFortress.Domain.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("APIUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("APIUserId");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Permission to read data",
                            Name = "CanRead"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Permission to write data",
                            Name = "CanWrite"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Permission to delete data",
                            Name = "CanDelete"
                        });
                });

            modelBuilder.Entity("ApiFortress.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Administrator",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Description = "User",
                            Name = "User"
                        });
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<int>("PermissionsId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("PermissionRole");

                    b.HasData(
                        new
                        {
                            PermissionsId = 1,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 2,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 3,
                            RolesId = 1
                        },
                        new
                        {
                            PermissionsId = 1,
                            RolesId = 2
                        });
                });

            modelBuilder.Entity("APIUserRole", b =>
                {
                    b.HasOne("ApiFortress.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiFortress.Domain.Entities.APIUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiFortress.Domain.Entities.Permission", b =>
                {
                    b.HasOne("ApiFortress.Domain.Entities.APIUser", null)
                        .WithMany("Permissions")
                        .HasForeignKey("APIUserId");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("ApiFortress.Domain.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiFortress.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiFortress.Domain.Entities.APIUser", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
