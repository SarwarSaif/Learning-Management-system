using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using customLms.Entities;

namespace customLms.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //user db entity. can be std and teachr

        public string name { get; set; }
        public bool isActive { get; set; }

        public bool isStudent { get; set; }





        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

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

        public DbSet<Catagory> catagories { get; set; }
        public DbSet<Part> parts { get; set; }
        public DbSet<ContentType> contentTypes { get; set; }

        public DbSet<Section> sections { get; set; }

        public DbSet<Content> contents { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<CourseLinkText> coursesLinkTexts { get; set; }
        public DbSet<Subscription> subscriptions { get; set; }
        public DbSet<CourseContent> courseContents { get; set; }
        public DbSet<SubscriptionOfCourse> subscriptionOfCourses { get; set; }
        public DbSet<StudentSubscription> studentSubscriptions{ get; set; }

    }
}