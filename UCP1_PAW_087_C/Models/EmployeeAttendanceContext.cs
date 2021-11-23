using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UCP1_PAW_087_C.Models
{
    public partial class EmployeeAttendanceContext : DbContext
    {
        public EmployeeAttendanceContext()
        {
        }

        public EmployeeAttendanceContext(DbContextOptions<EmployeeAttendanceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.IdAttendance);

                entity.Property(e => e.IdAttendance).HasColumnName("Id_Attendance");

                entity.Property(e => e.CheckInDatetime)
                    .HasColumnName("Check_In_Datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CheckOutDatetime)
                    .HasColumnName("Check_Out_Datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attendance_Users");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPosition);

                entity.Property(e => e.IdPosition)
                    .HasColumnName("Id_Position")
                    .ValueGeneratedNever();

                entity.Property(e => e.PositionName)
                    .HasColumnName("Position_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.Property(e => e.IdRole)
                    .HasColumnName("Id_Role")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .HasColumnName("Role_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPosition).HasColumnName("Id_Position");

                entity.Property(e => e.IdRole).HasColumnName("Id_Role");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPositionNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdPosition)
                    .HasConstraintName("FK_Users_Position");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_Users_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
