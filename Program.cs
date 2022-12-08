using Microsoft.EntityFrameworkCore;
using SchoolWebApp1.Data;
using SchoolWebApp1.Services;

namespace SchoolWebApp1 {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //dependency injections celé databáze:
            //lokální databáze
            //builder.Services.AddDbContext<ApplicationDbContext>(options => {
            //    options.UseSqlServer(builder.Configuration
            //        ["ConnectionStrings:SchoolDbConnection"]);
            //});
            builder.Services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString
                    ("monterea.somee.com"));
            });
            //dependency injections jednotlivých tabulek:
            builder.Services.AddScoped<StudentService>();
            builder.Services.AddScoped<SubjectService>();
            builder.Services.AddScoped<GradeService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}