using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalHouse.Implements;
using Microsoft.EntityFrameworkCore;
using AnimalHouse.Entities;
using AnimalHouse.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace AnimalHouse
{
    internal class Program
    {
        static void Main()
        {
            var sp = DIConfiguration.GetServiceProvider();
            //var animalService = sp.GetService<IAnimalService>()!;
            //bool isExit = false;
            //while (!isExit)
            //{
            //    Console.WriteLine("1. Add animal");
            //    Console.WriteLine("2. Show all animals");
            //    Console.WriteLine("3. Exit");
            //    Console.WriteLine("4. Delete animal");
            //    Console.WriteLine("5. Update animal");
            //    Console.Write("Enter the number: ");
            //    var key = Console.ReadKey();
            //    Console.WriteLine();
            //    switch (key.KeyChar)
            //    {
            //        case '1':
            //            Console.Write("Enter the name: ");
            //            var name = Console.ReadLine()!;
            //            Console.Write("Enter the age: ");
            //            var age = int.Parse(Console.ReadLine()!);
            //            Console.Write("Enter the description:");
            //            var description = Console.ReadLine()!;
            //            animalService.CreateAnimal(name, description, age);
            //            break;
            //        case '2':
            //            foreach (var animal0 in animalService.GetAllAnimals())
            //            {
            //                Console.WriteLine($"Name: {animal0.Name}, Age: {animal0.AGE}");
            //            }
            //            break;
            //        case '3':
            //            isExit = true;
            //            break;
            //        case '4':
            //            Console.Write("Enter the id: ");
            //            var id = int.Parse(Console.ReadLine()!);
            //            animalService.DeleteAnimal(id);
            //            break;
            //        case '5':
            //            Console.Write("Enter the id: ");
            //            var id1 = int.Parse(Console.ReadLine()!);
            //            var animal = animalService.GetAnimalById(id1);
            //            if (animal != null)
            //            {
            //                Console.Write("Enter the name: ");
            //                var name1 = Console.ReadLine()!;
            //                Console.Write("Enter the age: ");
            //                var age1 = int.Parse(Console.ReadLine()!);
            //                Console.Write("Enter the description:");
            //                var description1 = Console.ReadLine()!;

            //                animalService.UpdateAnimal(animal.Id, age1, name1, description1);
            //            }
            //            break;
            //    }
            //}
            var MedicalRecordsService = sp.GetService<IMedicalRecordsService>()!;
            bool isExit1 = false;
            while (!isExit1)
            {
                Console.WriteLine("1. Add Medical Record");
                Console.WriteLine("2. Show all Medical Records");
                Console.WriteLine("3. Exit");
                Console.WriteLine("4. Delete Medical Record");
                Console.WriteLine("5. Update Medical Record");
                Console.Write("Enter the number: ");
                var key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                    case '1':
                        Console.Write("Enter the animalId: ");
                        var animalId = int.Parse(Console.ReadLine()!);
                        Console.Write("Enter the description: ");
                        var description = Console.ReadLine()!;
                        Console.Write("Enter the date:");
                        var date = DateTime.Parse(Console.ReadLine()!);
                        MedicalRecordsService.CreateMedicalRecord(animalId, description, date);
                        break;
                    case '2':
                        foreach (var medicalRecord1 in MedicalRecordsService.GetAllMedicalRecords())
                        {
                            Console.WriteLine($"AnimalId: {medicalRecord1.AnimalId}, Description: {medicalRecord1.Diagnosis}, Date: {medicalRecord1.VisitDate}");
                        }
                        break;
                    case '3':
                        isExit1 = true;
                        break;
                    case '4':
                        Console.Write("Enter the id: ");
                        var id = int.Parse(Console.ReadLine()!);
                        MedicalRecordsService.DeleteMedicalRecord(id);
                        break;
                    case '5':
                        Console.Write("Enter the id: ");
                        var id1 = int.Parse(Console.ReadLine()!);
                        var medicalRecord = MedicalRecordsService.GetMedicalRecordById(id1);
                        if (medicalRecord != null)
                        {
                            Console.Write("Enter the animalId: ");
                            var animalId1 = int.Parse(Console.ReadLine()!);
                            Console.Write("Enter the description: ");
                            var description1 = Console.ReadLine()!;
                            Console.Write("Enter the date:");
                            var date1 = DateTime.Parse(Console.ReadLine()!);
                            MedicalRecordsService.UpdateMedicalRecord(medicalRecord.Id, animalId1, description1, date1);
                        }
                        break;
                }
            }

        }
    }
}
