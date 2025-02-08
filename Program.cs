using System.Text;
using System.Data.SqlClient;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;
EmployeeManager employeeManager = new EmployeeManager("*", "*", "*", "*");
Employee._id = employeeManager.LastKey();
employeeManager.LoadSqlConnectionToJson(@"Sql_Example\appsetting.json");
employeeManager.Create(Employee.Generate(fax:"+380993714545",PIB:"Valentinooo 54"));
employeeManager.Create(Employee.Generate(fax: "+380993714545", PIB: "Valentinooo 55"));
employeeManager.Create(Employee.Generate(fax: "+380993714545", PIB: "Valentinooo 56"));
Console.WriteLine(employeeManager.Read(1).PIB);
Console.WriteLine(employeeManager.Read(2).PIB);
employeeManager.Delete(1);
employeeManager.Update(new Employee(id:2,fax: "+380993714545", PIB: "Valentinooo 57"));