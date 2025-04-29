using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SmartClinic.Data;
using System.IO;

namespace SmartClinic.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // إنشاء الـ ConfigurationReader
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // تأكد من أن المسار الحالي هو المكان الصحيح
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // بنقرأ الـ appsettings.json
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // جلب الـ ConnectionString من الـ appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // تكوين DbContext باستخدام الـ ConnectionString
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

