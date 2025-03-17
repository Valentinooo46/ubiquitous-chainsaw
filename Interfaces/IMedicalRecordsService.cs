using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Entities;

namespace AnimalHouse.Interfaces
{
    interface IMedicalRecordsService
    {
        void CreateMedicalRecord(int animalId, string description, DateTime date);
        List<MedicalRecord> GetAllMedicalRecords();
        MedicalRecord? GetMedicalRecordById(int id);
        bool UpdateMedicalRecord(int id, int animalId, string description, DateTime date);
        bool DeleteMedicalRecord(int id);
    }
}
