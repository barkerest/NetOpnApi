﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetOpnApiBuilder;

namespace NetOpnApiBuilder.Migrations
{
    [DbContext(typeof(BuilderDb))]
    partial class BuilderDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiCommand", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClrName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<bool>("CommandChanged")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<int>("ControllerID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasNoPostBody")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasNoResponseBody")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NewCommand")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PostBodyDataType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PostBodyObjectTypeID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PostBodyPropertyName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<int?>("ResponseBodyDataType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ResponseBodyObjectTypeID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ResponseBodyPropertyName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Signature")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Skip")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SourceVersion")
                        .HasColumnType("TEXT");

                    b.Property<bool>("UsePost")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("PostBodyObjectTypeID");

                    b.HasIndex("ResponseBodyObjectTypeID");

                    b.HasIndex("ControllerID", "ApiName")
                        .IsUnique();

                    b.ToTable("ApiCommands");
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiController", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("ClrName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<int>("ModuleID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Skip")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ModuleID", "ApiName")
                        .IsUnique();

                    b.ToTable("ApiControllers");
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiModule", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("ClrName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<bool>("Skip")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SourceID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("SourceID", "ApiName")
                        .IsUnique();

                    b.ToTable("ApiModules");
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiObjectProperty", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<bool>("CanBeNull")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClrName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<int>("DataType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DataTypeObjectTypeID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImportSample")
                        .HasColumnType("TEXT");

                    b.Property<int>("ObjectTypeID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("DataTypeObjectTypeID");

                    b.HasIndex("ObjectTypeID", "ApiName")
                        .IsUnique();

                    b.ToTable("ApiObjectProperties");
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiObjectType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImportSample")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(1000);

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ApiObjectTypes");
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiQueryParam", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AllowNull")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("ClrName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<int>("CommandID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DataType")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CommandID", "ApiName")
                        .IsUnique();

                    b.ToTable("ApiQueryParams");
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiSource", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastSync")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(120);

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(32);

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ApiSources");
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiUrlParam", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AllowNull")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("ClrName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<int>("CommandID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DataType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CommandID", "Order")
                        .IsUnique();

                    b.ToTable("ApiUrlParams");
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.TestDevice", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.ToTable("TestDevice");
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiCommand", b =>
                {
                    b.HasOne("NetOpnApiBuilder.Models.ApiController", "Controller")
                        .WithMany("Commands")
                        .HasForeignKey("ControllerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetOpnApiBuilder.Models.ApiObjectType", "PostBodyObjectType")
                        .WithMany()
                        .HasForeignKey("PostBodyObjectTypeID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("NetOpnApiBuilder.Models.ApiObjectType", "ResponseBodyObjectType")
                        .WithMany()
                        .HasForeignKey("ResponseBodyObjectTypeID")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiController", b =>
                {
                    b.HasOne("NetOpnApiBuilder.Models.ApiModule", "Module")
                        .WithMany("Controllers")
                        .HasForeignKey("ModuleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiModule", b =>
                {
                    b.HasOne("NetOpnApiBuilder.Models.ApiSource", "Source")
                        .WithMany("Modules")
                        .HasForeignKey("SourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiObjectProperty", b =>
                {
                    b.HasOne("NetOpnApiBuilder.Models.ApiObjectType", "DataTypeObjectType")
                        .WithMany()
                        .HasForeignKey("DataTypeObjectTypeID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("NetOpnApiBuilder.Models.ApiObjectType", "ObjectType")
                        .WithMany("Properties")
                        .HasForeignKey("ObjectTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiQueryParam", b =>
                {
                    b.HasOne("NetOpnApiBuilder.Models.ApiCommand", "Command")
                        .WithMany("QueryParams")
                        .HasForeignKey("CommandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NetOpnApiBuilder.Models.ApiUrlParam", b =>
                {
                    b.HasOne("NetOpnApiBuilder.Models.ApiCommand", "Command")
                        .WithMany("UrlParams")
                        .HasForeignKey("CommandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
