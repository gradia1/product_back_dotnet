using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<MySqlService>(provider =>
    new MySqlService(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // เพิ่มโดเมนที่อนุญาต
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin"); // ใช้ CORS policy ที่กำหนด
app.UseAuthorization();
app.MapControllers(); // Map controllers to routes

app.Run();
