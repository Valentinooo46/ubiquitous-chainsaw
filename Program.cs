using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpecieProject.Implements;


namespace SpecieProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
           SpecieService specieService = new(new Repository<Specie>(new MyAppContext()));
            var specie = new Specie
            {
                Name = "Lion",
                Description = "The lion is a species in the family Felidae; it is a muscular, deep-chested cat with a short, rounded head, a reduced neck and round ears, and a hairy tuft at the end of its tail."
            };
            specieService.Add(specie);
            foreach (var item in specieService.GetAll())
            {
                Console.WriteLine(item);
            }
            specieService.Add(new Specie
            {
                Name = "",
                Description = ""
            });
        }
    }
}
