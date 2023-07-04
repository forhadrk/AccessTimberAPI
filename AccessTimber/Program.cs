using AccessTimber.DBContext;
using ATServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DatabaseContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddScoped<IDapper, Dapperr>();
// Add services to the container.

builder.Services.AddControllers();

var PMSAllowOrigin = "_myPMSOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: PMSAllowOrigin,
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://192.168.13.103:3000", "http://192.168.10.37:3000",
                                "https://apps.bitopibd.com/PMSLive/", "https://apps.bitopibd.com/PMS/", "*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting(); // Enable routing

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
