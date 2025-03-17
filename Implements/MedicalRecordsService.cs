using AnimalHouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Entities;

namespace AnimalHouse.Implements
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        private readonly IRepository<MedicalRecord> _medicalRecordRepository;
        public MedicalRecordsService(IRepository<MedicalRecord> medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }
        public void CreateMedicalRecord(int animalId, string description, DateTime date)
        {
            var utcDate = date.ToUniversalTime();
            var medicalRecord = new MedicalRecord
            {
                AnimalId = animalId,
                Diagnosis = description,
                VisitDate = utcDate
            };
            _medicalRecordRepository.Add(medicalRecord);
            _medicalRecordRepository.SaveChanges();
        }
        public bool DeleteMedicalRecord(int id)
        {
            var medicalRecord = _medicalRecordRepository.GetById(id);
            if (medicalRecord == null)
            {
                return false;
            }
            _medicalRecordRepository.Delete(medicalRecord);
            _medicalRecordRepository.SaveChanges();
            return true;
        }
        public List<MedicalRecord> GetAllMedicalRecords()
        {
            return _medicalRecordRepository.GetAll().ToList();
        }
        public MedicalRecord? GetMedicalRecordById(int id)
        {
            return _medicalRecordRepository.GetById(id);
        }
        public bool UpdateMedicalRecord(int id, int animalId, string description, DateTime date)
        {
            var utcDate = date.ToUniversalTime();
            var medicalRecord = _medicalRecordRepository.GetById(id);
            if (medicalRecord == null)
            {
                return false;
            }
            medicalRecord.AnimalId = animalId;
            medicalRecord.Diagnosis = description;
            medicalRecord.VisitDate = utcDate;
            _medicalRecordRepository.Update(medicalRecord);
            _medicalRecordRepository.SaveChanges();
            return true;
        }
    }
}
