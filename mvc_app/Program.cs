using Microsoft.EntityFrameworkCore;
using mvc_app.Services;

using mvc_app.DbContext;
using Microsoft.AspNetCore.Identity;

//namespace mvc_app
//{
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //Add service Products
        builder.Services.AddSingleton<IServiceProducts, ServiceProducts>();
        builder.Services.AddDbContext<ProductContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddDbContext<UserContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddControllersWithViews();
        builder.Services.AddDefaultIdentity<IdentityUser>(options => 
        { 
            //confirmed email
            options.SignIn.RequireConfirmedEmail = true; 
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequiredUniqueChars = 0;
        }).AddEntityFrameworkStores<UserContext>();

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
            );


        app.Run();
    }
}
//}
