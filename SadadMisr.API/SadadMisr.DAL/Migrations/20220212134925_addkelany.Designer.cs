﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SadadMisr.DAL;

namespace SadadMisr.DAL.Migrations
{
    [DbContext(typeof(SadadMasrDbContext))]
    [Migration("20220212134925_addkelany")]
    partial class addkelany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SadadMisr.DAL.Entities.Bill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ACIDnumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BillNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Containers20")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Containers40")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerMobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerTaxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSentToAswaq")
                        .HasColumnType("bit");

                    b.Property<int>("LineBillId")
                        .HasColumnType("int");

                    b.Property<long>("ManifestId")
                        .HasColumnType("bigint");

                    b.Property<int>("NumberOfContainers")
                        .HasColumnType("int");

                    b.Property<string>("POD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("POL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SCAC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShippingLineId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("kelany")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ManifestId");

                    b.HasIndex("ShippingLineId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Invoice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BillId")
                        .HasColumnType("bigint");

                    b.Property<string>("BillToParty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<decimal>("DiscountAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InvoiceCurrency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InvoiceTypeId")
                        .HasColumnType("int");

                    b.Property<string>("InvoiceTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFixed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSentToAswaq")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ItemsAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("LineInvoiceId")
                        .HasColumnType("int");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShippingLineId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("VatAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("ShippingLineId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Manifest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CallPort")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EstimatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<bool>("IsExport")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSentToAswaq")
                        .HasColumnType("bit");

                    b.Property<int>("LineManifestId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfBills")
                        .HasColumnType("int");

                    b.Property<int?>("ShippingAgencyId")
                        .HasColumnType("int");

                    b.Property<int?>("ShippingLineId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VesselName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VoyageNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShippingAgencyId");

                    b.HasIndex("ShippingLineId");

                    b.ToTable("Manifests");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Payment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AswaqPaymentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CommissionAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<long>("InvoiceId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSentToLine")
                        .HasColumnType("bit");

                    b.Property<int>("LinePaymentId")
                        .HasColumnType("int");

                    b.Property<decimal>("NetAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.Property<string>("TransactionNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("InvoiceId")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Port", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ports");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.ShippingAgency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShippingAgencies");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.ShippingLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShippingAgencyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShippingAgencyId");

                    b.ToTable("ShippingLines");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Bill", b =>
                {
                    b.HasOne("SadadMisr.DAL.Entities.Manifest", "Manifest")
                        .WithMany("Bills")
                        .HasForeignKey("ManifestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SadadMisr.DAL.Entities.ShippingLine", "ShippingLine")
                        .WithMany()
                        .HasForeignKey("ShippingLineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manifest");

                    b.Navigation("ShippingLine");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Invoice", b =>
                {
                    b.HasOne("SadadMisr.DAL.Entities.Bill", "Bill")
                        .WithMany("Invoices")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SadadMisr.DAL.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.HasOne("SadadMisr.DAL.Entities.ShippingLine", "ShippingLine")
                        .WithMany()
                        .HasForeignKey("ShippingLineId");

                    b.Navigation("Bill");

                    b.Navigation("Currency");

                    b.Navigation("ShippingLine");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Manifest", b =>
                {
                    b.HasOne("SadadMisr.DAL.Entities.ShippingAgency", "ShippingAgency")
                        .WithMany()
                        .HasForeignKey("ShippingAgencyId");

                    b.HasOne("SadadMisr.DAL.Entities.ShippingLine", "ShippingLine")
                        .WithMany()
                        .HasForeignKey("ShippingLineId");

                    b.Navigation("ShippingAgency");

                    b.Navigation("ShippingLine");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Payment", b =>
                {
                    b.HasOne("SadadMisr.DAL.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.HasOne("SadadMisr.DAL.Entities.Invoice", "Invoice")
                        .WithOne("Payment")
                        .HasForeignKey("SadadMisr.DAL.Entities.Payment", "InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.ShippingLine", b =>
                {
                    b.HasOne("SadadMisr.DAL.Entities.ShippingAgency", "ShippingAgency")
                        .WithMany()
                        .HasForeignKey("ShippingAgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShippingAgency");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Bill", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Invoice", b =>
                {
                    b.Navigation("Payment");
                });

            modelBuilder.Entity("SadadMisr.DAL.Entities.Manifest", b =>
                {
                    b.Navigation("Bills");
                });
#pragma warning restore 612, 618
        }
    }
}
