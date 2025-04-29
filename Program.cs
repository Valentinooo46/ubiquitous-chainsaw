
using Newtonsoft.Json;
using NovaPoshta.Context;
using System;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace NovaPoshta
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            const string API = "**";
            AreaContext[] areaContext = { new AreaContext(), new AreaContext(),new AreaContext() };
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Task> tasks = new();
            
            // Додавання областей
            var areaModel = new NPRequest
            {
                modelName = "AddressGeneral",
                calledMethod = "getAreas",
                methodProperties = null!,
                apiKey = API
            };
            var areaJson = JsonConvert.SerializeObject(areaModel);
            HttpClient client = new HttpClient();
            HttpContent areaContent = new StringContent(areaJson, Encoding.UTF8, "application/json");
            var areaResponse = await client.PostAsync("https://api.novaposhta.ua/v2.0/json/", areaContent);
            if (areaResponse.IsSuccessStatusCode)
            {
                var areaResponseString = await areaResponse.Content.ReadAsStringAsync();
                var areaData = JsonConvert.DeserializeObject<NovaPoshtaResponse<Area>>(areaResponseString)!;
                if (!areaContext[0].Areas.Any())
                {
                    tasks.Add(areaContext[0].Areas.AddRangeAsync(areaData.data));

                }
            }

            // Додавання міст
            var cityModel = new NPRequest
            {
                modelName = "Address",
                calledMethod = "getCities",
                methodProperties = new methodProperties("500"),
                apiKey = API
            };
            var cityJson = JsonConvert.SerializeObject(cityModel);
            HttpContent cityContent = new StringContent(cityJson, Encoding.UTF8, "application/json");
            var cityResponse = await client.PostAsync("https://api.novaposhta.ua/v2.0/json/", cityContent);
            if (cityResponse.IsSuccessStatusCode)
            {
                var cityResponseString = await cityResponse.Content.ReadAsStringAsync();
                var cityData = JsonConvert.DeserializeObject<NovaPoshtaResponse<City>>(cityResponseString)!;
                if (!areaContext[1].Cities.Any())
                {
                    tasks.Add(Task.Run(() => Parallel.For(0, cityData.data.Length, i =>
                    {
                        lock (areaContext[1])
                        {
                            
                            areaContext[1].Cities.Add(cityData.data[i]);
                        }






                    })));



                    
                }
            }
            
           
            // Додавання відділень
            var warehouseModel = new NPRequest
            {
                modelName = "AddressGeneral",
                calledMethod = "getWarehouses",
                methodProperties = new methodProperties { CityName = "Луцьк",Limit = "50"},
                apiKey = API
            };
            var warehouseJson = JsonConvert.SerializeObject(warehouseModel);
            HttpContent warehouseContent = new StringContent(warehouseJson, Encoding.UTF8, "application/json");
            var warehouseResponse = await client.PostAsync("https://api.novaposhta.ua/v2.0/json/", warehouseContent);
            if (warehouseResponse.IsSuccessStatusCode)
            {
                var warehouseResponseString = await warehouseResponse.Content.ReadAsStringAsync();
                var warehouseData = JsonConvert.DeserializeObject<NovaPoshtaResponse<Warehouse>>(warehouseResponseString);
                if (!areaContext[2].Warehouses.Any())
                {
                    tasks.Add(Task.Run(() => Parallel.For(0, warehouseData.data.Length, i =>
                    {
                        lock (areaContext[2])
                        {

                            areaContext[2].Warehouses.Add(warehouseData.data[i]);
                        }






                    })));
                }
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                await tasks[i];
                areaContext[i].SaveChanges();
            }
            stopwatch.Stop();
            Console.WriteLine($"Час виконання: {stopwatch.ElapsedMilliseconds} мс");
            client.Dispose();

        }
    }
}
