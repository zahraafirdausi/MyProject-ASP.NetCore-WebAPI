using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyProject.Context;
using MyProject.Controllers;
using MyProject.Repository;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyContext>(Options => Options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("MyProjectContext")));
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyProjectContext")));
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<AccountRepository>();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy",
//        builder => builder.WithOrigins("http://localhost:8082")
//                          .AllowAnyMethod()
//                          .AllowAnyHeader()
//                          .AllowCredentials());
//});

//builder.Services.AddDbContext<MyContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("MyProjectContext")); options.EnableSensitiveDataLogging(); });
//builder.Services.AddDbContext<MyContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("MyProjectContext"));
//    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//});

// Tambahan JWT 22/05/2023
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
//----------

//builder.Services.AddMvc();

var app = builder.Build();

//// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{

    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); // options added on 17-4-2023
        o.RoutePrefix = string.Empty;
    });

}
//tambahan
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//app.UseCors(builder => builder.WithMethods("PUT","DELETE","POST"));
//app.UseCors("CorsPolicy");

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthentication(); // add jwt 22-05-2023
app.UseAuthorization();
app.MapControllers();
app.Run();
