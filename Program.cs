using Microsoft.Extensions.DependencyInjection;
using OLX;
using OLX.Context;
using OLX.Interfaces;
using Newtonsoft.Json;
using OLX.Entities;

var serviceProvider = DIConfiguration.GetServiceProvider();
var categoryService = serviceProvider.GetService<INewCategoryService>();
var subcategoryService = serviceProvider.GetService<ISubCategoryService>();
////categoryService.CreateCategory("Electronics","Electronic devices");
////categoryService.CreateCategory("Clothes", "For babies,womens,and mens");
////categoryService.CreateCategory("Books", "For all ages");
////categoryService.CreateCategory("RAM", "supprot all  type of memory",1);
//categoryService.CreateCategory("T-Shirts", "For all ages",2);
//categoryService.CreateCategory("Pants", "For all ages", 2);
//categoryService.CreateCategory("Shirts", "For all ages", 2);
//categoryService.CreateCategory("Shorts", "For all ages", 2);
//categoryService.CreateCategory("Jeans", "For all ages", 2);
//categoryService.CreateCategory("Sweaters", "For all ages", 2);
//categoryService.CreateCategory("Female Jeans", "for women", 9);

string JsonText = File.ReadAllText("C:\\Users\\valea\\source\\repos\\OLX\\OLX\\JSONFile1.json");
var JsonObject = JsonConvert.DeserializeObject<JsObject>(JsonText)!;
var CategoryList = JsonObject.categories;
foreach (var category in CategoryList)
{
    categoryService.CreateCategory(category.Name, category.Slug, category.Subcategories);
    
}
