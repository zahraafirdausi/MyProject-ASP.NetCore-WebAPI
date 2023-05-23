using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Client
{


    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //tambahan session // Hapus Selasa 23-05-2023
            builder.Services.AddSession(options =>
            {
                ////add Config cookie 21-05-2023
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
                //options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            //-----


            //// Tambahan JWT 19/05/2023
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = true;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
            //        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
            //    };
            //});
            //------------------------
            //builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseSession(); //21-05-2023 // Hapus Selasa 23-05-2023

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                ///app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler("/Account/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //tambahan
            //app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            //app.UseCors(options => options.AllowAnyOrigin());

            //tambahan session //klo belum berhasil nanti sessionnya ada terus
            //builder.Services.AddSession();
            //app.UseSession();

            //// Tambahan JWT untuk request header/get header token 21-05-2023
            //app.Use(async (context, next) =>
            //{
            //    var token = context.Request.Cookies["jwt"];
            //    if (!string.IsNullOrEmpty(token))
            //    {
            //        context.Request.Headers.Add("Authorization", "Bearer " + token); 
            //    }
            //    await next();
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication(); // Tambahan JWT 19/05/2023
            app.UseAuthorization();
            //app.UseAuthentication(); // Tambahan JWT 19/05/2023


            //app.UseSession(); //add 28-04-2023

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Departments}/{action=Index}/{id?}");
                pattern: "{controller=Account}/{action=Login}/{id?}");
            //pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "departments",
            //        pattern: "departments/{action=index}",
            //        defaults: new { controller = "Departments" });

            //    //route lainnya
            //});

            app.Run();

        }
    }
}