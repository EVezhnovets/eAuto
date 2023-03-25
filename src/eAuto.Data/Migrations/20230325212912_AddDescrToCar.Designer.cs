﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eAuto.Data.Context;

#nullable disable

namespace eAuto.Data.Migrations
{
    [DbContext(typeof(EAutoContext))]
    [Migration("20230325212912_AddDescrToCar")]
    partial class AddDescrToCar
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.BodyTypeDataModel", b =>
                {
                    b.Property<int>("BodyTypeId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BodyTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BodyTypeId");

                    b.ToTable("BodyTypes");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.BrandDataModel", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.CarDataModel", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarId"));

                    b.Property<int>("BodyTypeId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("BrandId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<DateTime>("DateArrival")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("DriveTypeId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("EngineId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("GenerationId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("Odometer")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("PictureUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("PriceInitial")
                        .HasMaxLength(50)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TransmissionId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<DateTime>("Year")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.HasKey("CarId");

                    b.HasIndex("BodyTypeId");

                    b.HasIndex("BrandId");

                    b.HasIndex("DriveTypeId");

                    b.HasIndex("EngineId");

                    b.HasIndex("GenerationId");

                    b.HasIndex("ModelId");

                    b.HasIndex("TransmissionId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.DriveTypeDataModel", b =>
                {
                    b.Property<int>("DriveTypeId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DriveTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DriveTypeId");

                    b.ToTable("DriveTypes");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.EngineDataModel", b =>
                {
                    b.Property<int>("EngineId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EngineId"));

                    b.Property<int>("BrandId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("GenerationId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("IdentificationName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ModelId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("Power")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EngineId");

                    b.HasIndex("BrandId");

                    b.HasIndex("GenerationId");

                    b.HasIndex("ModelId");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.GenerationDataModel", b =>
                {
                    b.Property<int>("GenerationId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenerationId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GenerationId");

                    b.HasIndex("BrandId");

                    b.HasIndex("ModelId");

                    b.ToTable("GenerationDataModel");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.ModelDataModel", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModelId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ModelId");

                    b.HasIndex("BrandId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.TransmissionDataModel", b =>
                {
                    b.Property<int>("TransmissionId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransmissionId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TransmissionId");

                    b.ToTable("Transmissions");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.CarDataModel", b =>
                {
                    b.HasOne("eAuto.Data.Interfaces.DataModels.BodyTypeDataModel", "BodyType")
                        .WithMany()
                        .HasForeignKey("BodyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.BrandDataModel", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.DriveTypeDataModel", "DriveType")
                        .WithMany()
                        .HasForeignKey("DriveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.EngineDataModel", "Engine")
                        .WithMany()
                        .HasForeignKey("EngineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.GenerationDataModel", "Generation")
                        .WithMany()
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.ModelDataModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.TransmissionDataModel", "Transmission")
                        .WithMany()
                        .HasForeignKey("TransmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BodyType");

                    b.Navigation("Brand");

                    b.Navigation("DriveType");

                    b.Navigation("Engine");

                    b.Navigation("Generation");

                    b.Navigation("Model");

                    b.Navigation("Transmission");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.EngineDataModel", b =>
                {
                    b.HasOne("eAuto.Data.Interfaces.DataModels.BrandDataModel", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.GenerationDataModel", "Generation")
                        .WithMany()
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.ModelDataModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Generation");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.GenerationDataModel", b =>
                {
                    b.HasOne("eAuto.Data.Interfaces.DataModels.BrandDataModel", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAuto.Data.Interfaces.DataModels.ModelDataModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("eAuto.Data.Interfaces.DataModels.ModelDataModel", b =>
                {
                    b.HasOne("eAuto.Data.Interfaces.DataModels.BrandDataModel", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });
#pragma warning restore 612, 618
        }
    }
}
