﻿// <auto-generated />
using System;
using B1_task2.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace B1_task2.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241005084823_MakingChanges")]
    partial class MakingChanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("B1_task2.Server.Models.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AccountID"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ClassID")
                        .HasColumnType("integer");

                    b.Property<int>("FileID")
                        .HasColumnType("integer");

                    b.Property<int?>("ParentAccountID")
                        .HasColumnType("integer");

                    b.HasKey("AccountID");

                    b.HasIndex("ClassID");

                    b.HasIndex("FileID");

                    b.HasIndex("ParentAccountID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("B1_task2.Server.Models.Balance", b =>
                {
                    b.Property<int>("BalanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BalanceID"));

                    b.Property<int?>("AccountID")
                        .HasColumnType("integer");

                    b.Property<decimal>("Credit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Debit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("IncomingActive")
                        .HasColumnType("numeric");

                    b.Property<decimal>("IncomingPassive")
                        .HasColumnType("numeric");

                    b.Property<decimal>("OutgoingActive")
                        .HasColumnType("numeric");

                    b.Property<decimal>("OutgoingPassive")
                        .HasColumnType("numeric");

                    b.HasKey("BalanceID");

                    b.HasIndex("AccountID");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("B1_task2.Server.Models.Class", b =>
                {
                    b.Property<int>("ClassID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClassID"));

                    b.Property<int>("ClassBalanceID")
                        .HasColumnType("integer");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ClassID");

                    b.HasIndex("ClassBalanceID");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("B1_task2.Server.Models.File", b =>
                {
                    b.Property<int>("FileID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FileID"));

                    b.Property<int>("BalanceID")
                        .HasColumnType("integer");

                    b.Property<string>("FileInfo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("FileID");

                    b.HasIndex("BalanceID");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("B1_task2.Server.Models.Account", b =>
                {
                    b.HasOne("B1_task2.Server.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("B1_task2.Server.Models.File", "File")
                        .WithMany()
                        .HasForeignKey("FileID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("B1_task2.Server.Models.Account", "ParentAccount")
                        .WithMany()
                        .HasForeignKey("ParentAccountID");

                    b.Navigation("Class");

                    b.Navigation("File");

                    b.Navigation("ParentAccount");
                });

            modelBuilder.Entity("B1_task2.Server.Models.Balance", b =>
                {
                    b.HasOne("B1_task2.Server.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("B1_task2.Server.Models.Class", b =>
                {
                    b.HasOne("B1_task2.Server.Models.Balance", "ClassBalance")
                        .WithMany()
                        .HasForeignKey("ClassBalanceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassBalance");
                });

            modelBuilder.Entity("B1_task2.Server.Models.File", b =>
                {
                    b.HasOne("B1_task2.Server.Models.Balance", "Balance")
                        .WithMany()
                        .HasForeignKey("BalanceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Balance");
                });
#pragma warning restore 612, 618
        }
    }
}
