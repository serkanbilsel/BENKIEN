using Microsoft.EntityFrameworkCore;
using BENKIEN.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BENKIEN.Data
{
    public class DatabaseContext : DbContext // DatabaseContext sınıfına EntityFrameworkCore paketinden gelen DbContext sınıfından kalıtım alıyoruz böylece tüm veritabanı işlemlerini yapabileceğiz.
    {
        /*
         * Projede entity framewrok kullanacağız bu paketi projeye sağ tıklayıp nuget package manager browse yolunu izleyip önce sql server paketini yüklüyoruz sql veritabanı kullanabilmek için , sql server paketi içerisinde entity framework core paketinde bulunmaktadır.
        * Code first ile class larımızı kullanarak veritabanı oluşturma veya değiştirme işlemleri için de Tools paketini yüklüyoruz projeye.
         */
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<FooterSlider> FooterSliders { get; set; }
        public DbSet<MiddleSlider> MiddleSliders { get; set; }
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        public DbSet<ProductImage> ProductImages{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderItem> OrderItems{ get; set; }
        public DbSet<TopHeader> TopHeaders { get; set; }    
        public DbSet<User> Users { get; set; }

        public DbSet<Cart> Cart { get; set; } = default!;

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // bu metot veritabanı ayarlarını yapılandırabildiğimiz metot 
        {
            optionsBuilder.UseSqlServer(@"Server = 104.247.167.130\MSSQLSERVER2019;Database=arendij2_benkien;User Id=arendij2_benuser;Password=Benkien2400*;Encrypt=False;TrustServerCertificate=true;"); // UseSqlServer ile bu projede veritabanı olarak sql server kullanacağımızı belirttik " " içerisindeki alana connection string yani veritabanı bilgilerini yazıyoruz.
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<ProductImage>().HasKey(p => p.ProductId);

            modelBuilder.Entity<User>().HasData( // bu metot veritabanı oluştuktan sonra veritabanına kayıt eklememizi sağlıyor.
                new User //admin paneline giriş yapabilmek için en az 1 tane kullanıcı olması lazım ki bu bilgilerle giriş yapabilelim.
                {
                    Id = 1,
                    Email = "serkan@benkien.com",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "Admin",
                    Password = "12qk12qk"
                },
                new User 
                {
                    Id = 2,
                    Email = "eren@benkien.com",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "ErenAdmin",
                    Password = "20242024*"
                },
                new User
                {
                    Id = 3,
                    Email = "soner@benkien.com",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "SonerAdmin",
                    Password = "20242024*"
                },
                new User
                {
                    Id = 4,
                    Email = "zehra@benkien.com",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "ZehraAdmin",
                    Password = "20242024"
                }

                );
            // Fluent API
            modelBuilder.Entity<Category>().HasData( // kategoriler tablosuna da aşağıdaki kayıtları ekle
                new Category
                {
                    Id = 1,
                    Name = "Elektronik"
                },
                 new Category
                 {
                     Id = 2,
                     Name = "Kozmetik"
                 }
                );
            base.OnModelCreating(modelBuilder);
        }
     
      
        // Not :  buradaki yapılandırmayı da yaptıktan sonra Program.cs ye gidip orada databasecontext sınıfını programa tanımlamamız gerekiyor.
    }
}
