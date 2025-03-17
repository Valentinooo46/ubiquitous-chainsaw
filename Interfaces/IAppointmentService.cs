using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Entities;

namespace AnimalHouse.Interfaces
{
    interface IAppointmentService
    {
        bool CreateAppointment(int animalId, DateTime date, string description);
        List<Appointment> GetAllAppointments();
        Appointment? GetAppointmentById(int id);
        bool UpdateAppointment(int id, int animalId, DateTime date, string description);
        bool DeleteAppointment(int id);
    }
}
