using Microsoft.EntityFrameworkCore;
using AppointmentService.Clients;
using AppointmentService.Data;
using AppointmentService.Interfaces;
using AppointmentService.Messaging;
using AppointmentService.Repositories;
using AppointmentService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppointmentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentServices>();
builder.Services.AddSingleton<IEventPublisher, RabbitMQEventPublisher>();

builder.Services.AddHttpClient<IPatientServiceClient, PatientServiceClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PatientService"]
        ?? "http://localhost:5225/");
});

builder.Services.AddHttpClient<IDoctorServiceClient, DoctorServiceClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:DoctorService"]
        ?? "http://localhost:5246/");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
