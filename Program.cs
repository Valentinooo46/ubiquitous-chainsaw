using System.Text;
using System.Data.SqlClient;
using Sql_Example;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;
USERS_CH_Manager cH_Manager = new USERS_CH_Manager("*");
cH_Manager.INSERT_RANDOM_GENERATED_USERS(10000);
Console.WriteLine(cH_Manager.WATCHDOG_TIMER_SELECT_ALL_USERS());
cH_Manager.SEARCH_USER();
Console.WriteLine(cH_Manager.GetDatabaseSize());