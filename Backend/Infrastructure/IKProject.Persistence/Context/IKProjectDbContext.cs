using IK.Domain.Entities;
using IK.Domain.Entities.Concrete;
using IK.Domain.Entities.Identity;
using IKProject.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace IKProject.Persistence.Context
{
    public class IKProjectDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public IKProjectDbContext(DbContextOptions<IKProjectDbContext> options) : base(options)
        {
        }

        public DbSet<UserInformation> UserInformations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<ExpenseRequest> ExpenseRequests { get; set; }
        public DbSet<AdvanceRequest> AdvanceRequests { get; set; }
        public DbSet<AppUserLeaveRequest> AppUserLeaveRequests { get; set; }
        public DbSet<AppUserExpenseRequest> AppUserExpenseRequests { get; set; }
        public DbSet<AppUserAdvanceRequest> AppUserAdvanceRequests { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seeding
            var roles = new List<AppRole>
            {
                new AppRole
                {
                    Id = Guid.NewGuid(),
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new AppRole
                {
                    Id = Guid.NewGuid(),
                    Name = "manager",
                    NormalizedName = "MANAGER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new AppRole
                {
                    Id = Guid.NewGuid(),
                    Name = "employee",
                    NormalizedName = "EMPLOYEE",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            };

            modelBuilder.Entity<AppRole>().HasData(roles);

            var adminUserGuid = Guid.NewGuid();
            var adminUserInformationGuid = Guid.NewGuid();
            var managerUserGuid = Guid.NewGuid();
            var managerUserInformationGuid = Guid.NewGuid();
            var employeeUserGuid = Guid.NewGuid();
            var employeeUserInformationGuid = Guid.NewGuid();

            string currentDoc = Directory.GetCurrentDirectory();
            string photoPath = Path.Combine(currentDoc, @"image\Loginlogo.png");

            byte[] photoBytes = File.ReadAllBytes(photoPath);

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = adminUserGuid,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "ikburada@gmail.com",
                    UserName = "ikburada@gmail.com",
                    NormalizedEmail = "IKBURADA@GMAIL.COM",
                    NormalizedUserName = "IKBURADA@GMAIL.COM",
                    PhoneNumber = "05069064042",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "AAbb11.."),
                    MustChangePassword = true,
                    UserId = adminUserInformationGuid,


                });

            modelBuilder.Entity<UserInformation>().HasData(
                new UserInformation
                {
                    Id = adminUserInformationGuid,
                    FirstName = "Adminn",
                    LastName = "Userr",
                    TC = "67846016942",
                    SecondName = "Secondd",
                    SecondLastName = "Namee",
                    BirthDate = new DateTime(1990, 2, 1),
                    PlaceOfBirth = "Ankara",
                    StartDate = DateTime.Now,
                    EndDate = null,
                    Profession = "Engineerr",
                    Department = "ITT",
                    Address = "123 Main Stt",
                    AppUserId = adminUserGuid,
                    Photo = photoBytes,
                    IsActive = true,
                    Added = DateTime.Now
                });

            modelBuilder.Entity<AppUser>().HasData(
        new AppUser
        {
            Id = managerUserGuid,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "manager@example.com",
            UserName = "manager@example.com",
            NormalizedEmail = "MANAGER@EXAMPLE.COM",
            NormalizedUserName = "MANAGER@EXAMPLE.COM",
            PhoneNumber = "0987654321",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "AAbb11.."),
            MustChangePassword = false,
            UserId = managerUserInformationGuid,
        });

            modelBuilder.Entity<UserInformation>().HasData(
                new UserInformation
                {
                    Id = managerUserInformationGuid,
                    FirstName = "Jane",
                    LastName = "Smith",
                    TC = "98765432109",
                    SecondName = "",
                    SecondLastName = "",
                    BirthDate = new DateTime(1980, 6, 10),
                    PlaceOfBirth = "Village",
                    StartDate = DateTime.Now,
                    EndDate = null,
                    Profession = "Project Manager",
                    Department = "Management",
                    Address = "789 Main St",
                    AppUserId = managerUserGuid,
                    Photo = photoBytes,
                    IsActive = true,
                    Added = DateTime.Now,
                    Salary = 7000
                });

            // Seed Employee User
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = employeeUserGuid,
                    FirstName = "Kutayyy",
                    LastName = "Tasel",
                    Email = "employee@example.com",
                    UserName = "employee@example.com",
                    NormalizedEmail = "EMPLOYEE@EXAMPLE.COM",
                    NormalizedUserName = "EMPLOYEE@EXAMPLE.COM",
                    PhoneNumber = "0123456789",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "AAbb11.."),
                    MustChangePassword = false,
                    UserId = employeeUserInformationGuid,
                });

            modelBuilder.Entity<UserInformation>().HasData(
                new UserInformation
                {
                    Id = employeeUserInformationGuid,
                    FirstName = "Berkay",
                    LastName = "Tasel",
                    TC = "12345678901",
                    SecondName = "",
                    SecondLastName = "",
                    BirthDate = new DateTime(1985, 5, 15),
                    PlaceOfBirth = "Town",
                    StartDate = DateTime.Now,
                    EndDate = null,
                    Profession = "Analyst",
                    Department = "HR",
                    Address = "456 Another St",
                    AppUserId = employeeUserGuid,
                    Photo = photoBytes,
                    IsActive = true,
                    Added = DateTime.Now,
                    Salary = 5000
                });

            var adminRoleId = roles.First(r => r.Name == "admin").Id;
            var managerRoleId = roles.First(r => r.Name == "manager").Id;
            var employeeRoleId = roles.First(r => r.Name == "employee").Id;

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserGuid
                }
            );

            #endregion

            modelBuilder.Entity<AppUser>(entity =>
                {
                    entity.HasOne(au => au.UserInformation)
                          .WithOne(u => u.AppUser)
                          .HasForeignKey<AppUser>(au => au.UserId)
                          .OnDelete(DeleteBehavior.Cascade);
                    entity.HasOne(au => au.Company)
                          .WithMany(c => c.AppUser)
                          .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity<UserInformation>(entity =>
            {
                entity.HasOne(e => e.AppUser)
                      .WithOne(u => u.UserInformation)
                      .HasForeignKey<UserInformation>(e => e.AppUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>()
                .HasKey(login => new { login.LoginProvider, login.ProviderKey });

            modelBuilder.Entity<Company>()
               .HasMany(c => c.AppUser)
               .WithOne(e => e.Company)
               .HasForeignKey(e => e.CompanyId);


            modelBuilder.Entity<AppUserLeaveRequest>()
               .HasKey(al => new { al.AppUserId, al.LeaveRequestId });

            modelBuilder.Entity<AppUserLeaveRequest>()
                .HasOne(al => al.AppUser)
                .WithMany(au => au.AppUserLeaveRequests)
                .HasForeignKey(al => al.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUserLeaveRequest>()
                .HasOne(al => al.LeaveRequest)
                .WithMany(lr => lr.AppUserLeaveRequests)
                .HasForeignKey(al => al.LeaveRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AppUserExpenseRequest>()
                .HasKey(ae => new { ae.AppUserId, ae.ExpenseRequestId });

            modelBuilder.Entity<AppUserExpenseRequest>()
                .HasOne(ae => ae.AppUser)
                .WithMany(au => au.AppUserExpenseRequests)
                .HasForeignKey(ae => ae.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUserExpenseRequest>()
                .HasOne(ae => ae.ExpenseRequest)
                .WithMany(er => er.AppUserExpenseRequests)
                .HasForeignKey(ae => ae.ExpenseRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AppUserAdvanceRequest>()
                .HasKey(aa => new { aa.AppUserId, aa.AdvanceRequestId });

            modelBuilder.Entity<AppUserAdvanceRequest>()
                .HasOne(aa => aa.AppUser)
                .WithMany(au => au.AppUserAdvanceRequests)
                .HasForeignKey(aa => aa.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUserAdvanceRequest>()
                .HasOne(aa => aa.AdvanceRequest)
                .WithMany(ar => ar.AppUserAdvanceRequests)
                .HasForeignKey(aa => aa.AdvanceRequestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LeaveRequest>(entity =>
            {
                entity.HasMany(lr => lr.AppUserLeaveRequests)
                      .WithOne(al => al.LeaveRequest)
                      .HasForeignKey(al => al.LeaveRequestId);
            });

            modelBuilder.Entity<ExpenseRequest>(entity =>
            {
                entity.HasMany(er => er.AppUserExpenseRequests)
                      .WithOne(ae => ae.ExpenseRequest)
                      .HasForeignKey(ae => ae.ExpenseRequestId);
                entity.Property(er => er.ExpenseAmount)
                      .HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<AdvanceRequest>(entity =>
            {
                entity.HasMany(ar => ar.AppUserAdvanceRequests)
                      .WithOne(aa => aa.AdvanceRequest)
                      .HasForeignKey(aa => aa.AdvanceRequestId);
                entity.Property(ar => ar.AdvanceAmount)
                      .HasColumnType("decimal(18,2)");
            });


            base.OnModelCreating(modelBuilder);

        }
    }
}

