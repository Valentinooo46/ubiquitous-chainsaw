using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalHouse.Implements
{
    public class AppointmentService : Interfaces.IAppointmentService
    {
        private readonly Interfaces.IRepository<Entities.Appointment> _appointmentRepository;
        public AppointmentService(Interfaces.IRepository<Entities.Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public bool CreateAppointment(int animalId, DateTime date, string description)
        {
            var appointment = new Entities.Appointment
            {
                AnimalId = animalId,
                Date = date,
                Description = description
            };
            _appointmentRepository.Add(appointment);
            _appointmentRepository.SaveChanges();
            return true;
        }
        public List<Entities.Appointment> GetAllAppointments()
        {
            return _appointmentRepository.GetAll().ToList();
        }
        public Entities.Appointment? GetAppointmentById(int id)
        {
            return _appointmentRepository.GetById(id);
        }
        public bool UpdateAppointment(int id, int animalId, DateTime date, string description)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                return false;
            }
            appointment.AnimalId = animalId;
            appointment.Date = date;
            appointment.Description = description;
            _appointmentRepository.Update(appointment);
            _appointmentRepository.SaveChanges();
            return true;
        }
        public bool DeleteAppointment(int id)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                return false;
            }
            _appointmentRepository.Delete(appointment);
            _appointmentRepository.SaveChanges();
            return true;
        }
    }
}
