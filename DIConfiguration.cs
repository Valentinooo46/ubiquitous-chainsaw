using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OLX.Implements;
using Microsoft.Extensions.DependencyInjection;


namespace OLX
{
    public static class DIConfiguration
    {
        public static IServiceProvider GetServiceProvider()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddDbContext<Context.OLXContext>(options =>
                    options.UseNpgsql("***"));
                services.AddScoped(typeof(Interfaces.IRepository<>), typeof(Implements.Repository<>));
                services.AddScoped<Interfaces.ICategoryService, Implements.CategoryService>();
                services.AddScoped<Interfaces.IProductService, Implements.ProductService>();
                services.AddScoped<Interfaces.INewCategoryService, Implements.NewCategoryService>();
                services.AddScoped<Interfaces.ISubCategoryService, Implements.SubCategoryService>();
            }).Build();
            var score = host.Services.CreateScope();
            return score.ServiceProvider;
        }
    }
}
