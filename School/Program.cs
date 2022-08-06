using Microsoft.EntityFrameworkCore;
using School.Persistance;
using School.Repositories;
using School.Services;

var builder = WebApplication.CreateBuilder(args); 
//builder.Services.AddDbContext<SchoolDbContext>(opt => opt.UseInMe9moryDatabase("SchoolDb"));
builder.Services.AddDbContext<SchoolDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbDev")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
     {
         options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
         options.RoutePrefix = string.Empty;
     });
}

app.MapControllers();
app.Run();

