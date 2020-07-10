﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThreeI.TelegramBot.Data;

namespace ThreeI.TelegramBot.Data.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    partial class SqliteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("ThreeI.TelegramBot.Core.DialogState", b =>
                {
                    b.Property<int>("DialogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("dialog_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Block")
                        .HasColumnName("block")
                        .HasColumnType("TEXT");

                    b.Property<int>("Category")
                        .HasColumnName("category")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChatPhase")
                        .HasColumnName("chat_phase")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Confirmation")
                        .HasColumnName("confirmation")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSupportMode")
                        .HasColumnName("is_support_mode")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastActive")
                        .HasColumnName("last_active")
                        .HasColumnType("TEXT");

                    b.Property<string>("Unit")
                        .HasColumnName("unit")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("TEXT");

                    b.HasKey("DialogId");

                    b.ToTable("dialog_states");
                });

            modelBuilder.Entity("ThreeI.TelegramBot.Core.FaultReport", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("report_id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateLogged")
                        .HasColumnName("date_logged")
                        .HasColumnType("TEXT");

                    b.Property<int?>("dialog_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReportId");

                    b.HasIndex("dialog_id");

                    b.ToTable("fault_reports");
                });

            modelBuilder.Entity("ThreeI.TelegramBot.Core.FaultReport", b =>
                {
                    b.HasOne("ThreeI.TelegramBot.Core.DialogState", "DialogState")
                        .WithMany("FaultReports")
                        .HasForeignKey("dialog_id");
                });
#pragma warning restore 612, 618
        }
    }
}
