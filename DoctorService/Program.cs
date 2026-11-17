using DoctorService.Data;
using DoctorService.Interfaces;
using DoctorService.Messaging;
using DoctorService.Repositories;
using DoctorService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient("appointment-service", client =>
    client.BaseAddress = new Uri(builder.Configuration["Services:AppointmentService"] ?? "http://localhost:5281/"));
builder.Services.AddDbContext<DoctorDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorServices>();
builder.Services.AddSingleton<ReceivedAppointmentStore>();
builder.Services.AddHostedService<AppointmentCreatedConsumer>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
