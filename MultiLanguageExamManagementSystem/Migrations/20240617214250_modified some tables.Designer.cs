﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MultiLanguageExamManagementSystem.Data;

#nullable disable

namespace MultiLanguageExamManagementSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240617214250_modified some tables")]
    partial class modifiedsometables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Exam", b =>
                {
                    b.Property<int>("ExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProfessorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ExamId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.ExamRequest", b =>
                {
                    b.Property<int>("ExamRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamRequestId"));

                    b.Property<DateTime?>("ApprovalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("AttemptCount")
                        .HasColumnType("int");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<string>("ProfessorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExamRequestId");

                    b.HasIndex("ExamId");

                    b.ToTable("ExamRequests");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Exam_Question", b =>
                {
                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("ExamId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Exam_Questions");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.LocalizationResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BeautifiedNamespace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Namespace")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("Namespace", "Key", "LanguageId")
                        .IsUnique();

                    b.ToTable("LocalizationResources");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionId"));

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OptionC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfessorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("QuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.TakenExam", b =>
                {
                    b.Property<int>("TakenExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TakenExamId"));

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TakenExamId");

                    b.HasIndex("ExamId");

                    b.ToTable("TakenExams");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Auth0Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.ExamRequest", b =>
                {
                    b.HasOne("MultiLanguageExamManagementSystem.Models.Entities.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Exam_Question", b =>
                {
                    b.HasOne("MultiLanguageExamManagementSystem.Models.Entities.Exam", "Exam")
                        .WithMany("Exam_Questions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultiLanguageExamManagementSystem.Models.Entities.Question", "Question")
                        .WithMany("Exam_Questions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Language", b =>
                {
                    b.HasOne("MultiLanguageExamManagementSystem.Models.Entities.Country", "Country")
                        .WithMany("Languages")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.LocalizationResource", b =>
                {
                    b.HasOne("MultiLanguageExamManagementSystem.Models.Entities.Language", "Language")
                        .WithMany("LocalizationResources")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.TakenExam", b =>
                {
                    b.HasOne("MultiLanguageExamManagementSystem.Models.Entities.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Country", b =>
                {
                    b.Navigation("Languages");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Exam", b =>
                {
                    b.Navigation("Exam_Questions");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Language", b =>
                {
                    b.Navigation("LocalizationResources");
                });

            modelBuilder.Entity("MultiLanguageExamManagementSystem.Models.Entities.Question", b =>
                {
                    b.Navigation("Exam_Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
