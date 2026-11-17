using Microsoft.EntityFrameworkCore;
using PatientService.Data;
using PatientService.Interfaces;
using PatientService.Messaging;
using PatientService.Repositories;
using PatientService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient("appointment-service", client =>
    client.BaseAddress = new Uri(builder.Configuration["Services:AppointmentService"] ?? "http://localhost:5281/"));
builder.Services.AddDbContext<PatientDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientServices>();
builder.Services.AddSingleton<ReceivedAppointmentStore>();
builder.Services.AddHostedService<AppointmentCreatedConsumer>();
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
