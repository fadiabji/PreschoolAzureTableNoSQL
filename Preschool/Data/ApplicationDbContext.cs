using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Preschool.Models;

namespace Preschool.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        ////This in order to create database from here, don't follow the DbSet method anymore
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    //Children
        //    modelBuilder.Entity<Child>().HasData(new Child
        //    {
        //        Id = 1,
        //        FirstName = "Layaan",
        //        LastName = "Test",
        //        FatherName = "Fadi",
        //        MotherName = "Maryam", 
        //        DateOfBirth = DateTime.Parse("01,01,2019"),
        //        EnrolDate = DateTime.Parse("01,01,2023"),
        //        ClassroomId = 1,
        //        IsActive = true,

        //    });
        //    modelBuilder.Entity<Child>().HasData(new Child
        //    {
        //        Id = 2,
        //        FirstName = "Sally",
        //        LastName = "Test",
        //        FatherName = "Fadi",
        //        MotherName = "Maryam",
        //        DateOfBirth = DateTime.Parse("01,01,2017"),
        //        EnrolDate = DateTime.Parse("01,01,2022"),
        //        ClassroomId = 1,
        //        IsActive = true,

        //    });
            
        //    //ClassRoom
        //    modelBuilder.Entity<Classroom>().HasData(new Classroom
        //    {
        //        Id = 1,
        //        Name = "ClassRoom 1",
        //        Color = "Pink",
        //        Icon = "fas fa-couch"

        //    });


        //    modelBuilder.Entity<Classroom>().HasData(new Classroom
        //    {
        //        Id = 2,
        //        Name = "ClassRoom 2",
        //        Color = "Blue",
        //        Icon = "fas fa-couch"

        //    });
            
         

        //    //Add users
        //    modelBuilder.Entity<User>().HasData(new User
        //    {
        //        Id = "1",
        //        FirstName = "Fadi",
        //        LastName= "Test"
                
        //    });
        //    modelBuilder.Entity<User>().HasData(new User
        //    {
        //        Id = "2",
        //        FirstName = "Maryam",
        //        LastName = "Test"

        //    });

        //    //Teacher
        //    modelBuilder.Entity<Teacher>().HasData(new Teacher
        //    {
        //       Id= 1,
        //       FirstName= "Teacher1",
        //       LastName= "Test",
        //       DateOfBirth = DateTime.Parse("10.10.1987"),
        //       RegistedAt = DateTime.Parse("10,10,2022"),
        //       ClassroomId= 1,
        //       IsActive = true,
        //    });
        //    modelBuilder.Entity<Teacher>().HasData(new Teacher
        //    {
        //        Id = 2,
        //        FirstName = "Teacher2",
        //        LastName = "Test",
        //        DateOfBirth = DateTime.Parse("10.10.1987"),
        //        RegistedAt = DateTime.Parse("10,10,2022"),
        //        ClassroomId = 2,
        //        IsActive = true,
        //    });

        //    modelBuilder.Entity<DocumentsCopies>().HasData(new DocumentsCopies
        //    {
        //        Id = 1,
        //        ImageFile = "wwwroot/DocumentsCopies/NikeTShirt.jpg",
        //    });
        //    modelBuilder.Entity<DocumentsCopies>().HasData(new DocumentsCopies
        //    {
        //        Id = 2,
        //        ImageFile = "wwwroot/DocumentsCopies/RainCoat.jpg",
        //    });
        //    modelBuilder.Entity<DocumentsCopies>().HasData(new DocumentsCopies
        //    {
        //        Id = 3,
        //        ImageFile = "wwwroot/DocumentsCopies/VersityJacket.jpg",
        //    });


        //    modelBuilder.Entity<TeacherDocuemtnsCopy>().HasData(new TeacherDocuemtnsCopy
        //    {
        //        Id = 1,
        //        TeacherId = 1,
        //        DocumentsCopyId =1

        //    });
        //    modelBuilder.Entity<TeacherDocuemtnsCopy>().HasData(new TeacherDocuemtnsCopy
        //    {
        //        Id = 2,
        //        TeacherId = 2,
        //        DocumentsCopyId = 2
        //    });
        //    modelBuilder.Entity<ChildDocumentsCopy>().HasData(new ChildDocumentsCopy
        //    {
        //        Id = 3,
        //        ChildId = 1,
        //        DocumentsCopyId = 3
        //    });

        //}

        public DbSet<Child> Childern { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Classroom> Classrooms  { get; set; }

        public DbSet<DocumentsCopies> DocumentsImages { get; set; }

        public DbSet<TeacherDocuemtnsCopy> TeacherDocuemtnsCopies { get; set; }

        public DbSet<ChildDocumentsCopy> ChildDocumentsCopies { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }

        public DbSet<Attendance> Attendances  { get; set; }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Expense> Expenses { get; set; }


        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<InvoiceSubscriptionType> InvoiceSubscriptionTypes { get; set; }

        


    }

}