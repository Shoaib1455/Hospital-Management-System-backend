using System;
using System.Collections.Generic;

namespace AppointmentService.Models;

public partial class Appointment
{
    public Guid Id { get; set; }

    public Guid Patientid { get; set; }

    public Guid Doctorid { get; set; }

    public DateOnly Appointmentdate { get; set; }

    public TimeOnly Appointmenttime { get; set; }

    public string Status { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }
}
