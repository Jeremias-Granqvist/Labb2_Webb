using Labb2_API;
using Labb2_API.Controllers;
using Labb2_Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Labb2_Infrastructure;
using Labb2_Shared.Interfaces;
using Labb2_Infrastructure.Repositories;
using Labb2_Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<StoreContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IReferenceRepository, ReferenceRepository>();
builder.Services.AddScoped<IReferenceService, ReferenceService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("https://localhost:5188")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "frontend-only", configurePolicy: policy =>
//    {
//        policy.AllowAnyHeader();
//        policy.AllowAnyMethod();
//        policy.AllowAnyOrigin();
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

//app.UseCors("frontend-only");

app.UseAuthorization();
app.MapControllers();

app.Run();
