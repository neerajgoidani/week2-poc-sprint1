using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace IdentityFrame.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public string StudioName { get; set; }


        //  public Studio Studio { get; set; }
        
        
        public virtual StudioModel Studio { get; set; }
        public int StudioId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }


    // user defined table - studio
  


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<StudioModel> Studios { get; set; }
        

      //  public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Employee").Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<IdentityUserRole>().ToTable("EmployeeRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("EmployeeLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("EmployeeClaims").Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(p => p.Id).HasColumnName("RolesId");
            modelBuilder.Entity<StudioModel>().ToTable("Studio");
            //  modelBuilder.Entity<ApplicationUser>().HasRequired(n => n.Studio).WithMany( a =>  a).HasForeignKey(n => n.StudioId);

            modelBuilder.Entity<ApplicationUser>()
           .HasRequired<StudioModel>(s => s.Studio)
           .WithMany(g => g.ApplicationUsers)
           .HasForeignKey(s => s.StudioId);


         //   modelBuilder.Entity<Studio>().has
         //     modelBuilder.Entity<Studio>().ToTable("Studio").Property(p => p.Id).HasColumnName("StudentId");
        }
    }
}