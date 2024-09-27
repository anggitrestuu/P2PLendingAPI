﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using P2PLendingAPI.Data;

#nullable disable

namespace P2PLendingAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240927171846_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("P2PLendingAPI.Models.Funding", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FundedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LenderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LoanId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LenderId");

                    b.HasIndex("LoanId");

                    b.ToTable("Fundings");
                });

            modelBuilder.Entity("P2PLendingAPI.Models.Loan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BorrowerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("LenderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.HasIndex("LenderId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("P2PLendingAPI.Models.Repayment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BalanceAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LoanId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PaidAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("RepaidAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RepaidStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LoanId");

                    b.ToTable("Repayments");
                });

            modelBuilder.Entity("P2PLendingAPI.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("P2PLendingAPI.Models.Funding", b =>
                {
                    b.HasOne("P2PLendingAPI.Models.User", "Lender")
                        .WithMany()
                        .HasForeignKey("LenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("P2PLendingAPI.Models.Loan", "Loan")
                        .WithMany()
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lender");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("P2PLendingAPI.Models.Loan", b =>
                {
                    b.HasOne("P2PLendingAPI.Models.User", "Borrower")
                        .WithMany()
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("P2PLendingAPI.Models.User", "Lender")
                        .WithMany()
                        .HasForeignKey("LenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Borrower");

                    b.Navigation("Lender");
                });

            modelBuilder.Entity("P2PLendingAPI.Models.Repayment", b =>
                {
                    b.HasOne("P2PLendingAPI.Models.Loan", "Loan")
                        .WithMany()
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Loan");
                });
#pragma warning restore 612, 618
        }
    }
}
