using Microsoft.EntityFrameworkCore;
using ClassEnrollmentGrades01.Models;

namespace ClassEnrollmentGrades01.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public string DbPath { get; }

        public ApplicationContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "enrollments.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка составного ключа для Enrollment
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            // Настройка каскадного удаления
            modelBuilder.Entity<Enrollment>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Включаем внешние ключи при создании базы
            modelBuilder.HasAnnotation("Sqlite:Foreign Keys", true);
        }

        public void InitializeDatabase()
        {
            Database.EnsureDeleted();
            
            // Создаем базу и таблицы
            Database.ExecuteSqlRaw(@"
                PRAGMA foreign_keys = OFF;
                
                CREATE TABLE IF NOT EXISTS Courses (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Students (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    [Group] TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Enrollments (
                    StudentId INTEGER NOT NULL,
                    CourseId INTEGER NOT NULL,
                    Grade REAL NOT NULL,
                    GradeDate TEXT NOT NULL,
                    PRIMARY KEY (StudentId, CourseId),
                    FOREIGN KEY (StudentId) REFERENCES Students (Id) ON DELETE CASCADE,
                    FOREIGN KEY (CourseId) REFERENCES Courses (Id) ON DELETE CASCADE
                );

                PRAGMA foreign_keys = ON;
            ");
        }
    }
} 