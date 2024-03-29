﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Data.Migrations
{
    [DbContext(typeof(MySqlDbContext))]
    [Migration("20200722190059_AddNamesToDialogState")]
    partial class AddNamesToDialogState
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ThreeI.TelegramBot.Core.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("category_id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(25) CHARACTER SET utf8mb4")
                        .HasMaxLength(25);

                    b.Property<int>("SupervisorId")
                        .HasColumnName("supervisor_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupervisorId")
                        .IsUnique();

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Electrical related issues",
                            Name = "Electricity",
                            SupervisorId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "Water related issues",
                            Name = "Plumbing",
                            SupervisorId = 2
                        },
                        new
                        {
                            Id = 3,
                            Description = "Painting related issues",
                            Name = "Paint",
                            SupervisorId = 3
                        },
                        new
                        {
                            Id = 4,
                            Description = "Walls & Ceilings related issues",
                            Name = "Walls & Ceilings",
                            SupervisorId = 4
                        },
                        new
                        {
                            Id = 5,
                            Description = "Carpentry",
                            Name = "Carpentry",
                            SupervisorId = 5
                        },
                        new
                        {
                            Id = 6,
                            Description = "Other",
                            Name = "Other",
                            SupervisorId = 6
                        });
                });

            modelBuilder.Entity("ThreeI.TelegramBot.Core.DialogState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("dialog_id")
                        .HasColumnType("int");

                    b.Property<string>("Block")
                        .HasColumnName("block")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ChatPhase")
                        .HasColumnName("chat_phase")
                        .HasColumnType("int");

                    b.Property<int>("Confirmation")
                        .HasColumnName("confirmation")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<bool>("IsSupportMode")
                        .HasColumnName("is_support_mode")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastActive")
                        .HasColumnName("last_active")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("Unit")
                        .HasColumnName("unit")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("category_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("category_id");

                    b.ToTable("dialog_states");
                });

            modelBuilder.Entity("ThreeI.TelegramBot.Core.FaultReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("report_id")
                        .HasColumnType("int");

                    b.Property<string>("Block")
                        .IsRequired()
                        .HasColumnName("block")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateLogged")
                        .HasColumnName("date_logged")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.Property<int?>("DialogStateId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnName("unit")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DialogStateId");

                    b.ToTable("reports");
                });

            modelBuilder.Entity("ThreeI.TelegramBot.Core.Supervisor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("supervisor_id")
                        .HasColumnType("int");

                    b.Property<long>("ChatId")
                        .HasColumnName("chat_id")
                        .HasColumnType("bigint");

                    b.Property<string>("FullName")
                        .HasColumnName("full_name")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int>("TelegramUserId")
                        .HasColumnName("telegram_user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("supervisors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChatId = 0L,
                            TelegramUserId = 0
                        },
                        new
                        {
                            Id = 2,
                            ChatId = 0L,
                            TelegramUserId = 0
                        },
                        new
                        {
                            Id = 3,
                            ChatId = 0L,
                            TelegramUserId = 0
                        },
                        new
                        {
                            Id = 4,
                            ChatId = 0L,
                            TelegramUserId = 0
                        },
                        new
                        {
                            Id = 5,
                            ChatId = 0L,
                            TelegramUserId = 0
                        },
                        new
                        {
                            Id = 6,
                            ChatId = 0L,
                            TelegramUserId = 0
                        });
                });

            modelBuilder.Entity("ThreeI.TelegramBot.Core.Category", b =>
                {
                    b.HasOne("ThreeI.TelegramBot.Core.Supervisor", "Supervisor")
                        .WithOne("Category")
                        .HasForeignKey("ThreeI.TelegramBot.Core.Category", "SupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ThreeI.TelegramBot.Core.DialogState", b =>
                {
                    b.HasOne("ThreeI.TelegramBot.Core.Category", "Category")
                        .WithMany("Dialog")
                        .HasForeignKey("category_id");
                });

            modelBuilder.Entity("ThreeI.TelegramBot.Core.FaultReport", b =>
                {
                    b.HasOne("ThreeI.TelegramBot.Core.Category", "Category")
                        .WithMany("Report")
                        .HasForeignKey("CategoryId");

                    b.HasOne("ThreeI.TelegramBot.Core.DialogState", "DialogState")
                        .WithMany("FaultReports")
                        .HasForeignKey("DialogStateId");
                });
#pragma warning restore 612, 618
        }
    }
}
