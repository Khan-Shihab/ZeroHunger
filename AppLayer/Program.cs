using BLL;
using BLL.Services;
using DAL.EF;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<EmployeeRepo>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<RestaurantRepo>();
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<CollectRequestRepo>();
builder.Services.AddScoped<CollectRequestService>();
builder.Services.AddScoped<DistributionRecordRepo>();
builder.Services.AddScoped<FoodItemRepo>();
builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<StatusLogRepo>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<ZeroHungerDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
