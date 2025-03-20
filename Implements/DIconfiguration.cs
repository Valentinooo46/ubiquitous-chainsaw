using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using AnimalHouse.Implements;
using AnimalHouse.Interfaces;



namespace AnimalHouse.Implements
{
    public static class DIConfiguration
    {
        public static IServiceProvider GetServiceProvider()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    //Прописумо налаштування для різних обєктів
                    services.AddDbContext<MyAppContext>(options =>
                        options.UseNpgsql("*"));

                    //Якщо у коді потрібно репозіторій, то на основі IRepository буде створюватися
                    //Repository
                    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                    services.AddScoped<IAnimalService, AnimalService>();
                    services.AddScoped<ICustomerService, CustomerService>();
                    services.AddScoped<IAppointmentService, AppointmentService>();
                    services.AddScoped<IMedicalRecordsService, MedicalRecordsService>();
                    services.AddScoped<ICustomerAdoptInfoService, CustomerAdoptInfoService>();
                    services.AddScoped<IShelterService, ShelterService>();
                    services.AddScoped<IEmployeeService, EmployeeService>();

                })
                .Build();

            //Створити об'єкт score, який згідно налаштувань може видавти різні обєкти по DI
            var score = host.Services.CreateScope();
            //Отримуємо провайдер, який може видавати потрібні нам об'єкти
            return score.ServiceProvider;
        }
    }
}
