using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SamuraiApp.UI
{
    internal class Program
    {
        private static readonly SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            //GetSamurais("Before Add:");
            //AddSamurais("Julie", "Sampson");
            //AddVariousTypes();
            //InsertNewSamuraiWithAQuote("Jet frate", "Paul");
            getSamuraiDetails();
            Console.Write("Press any key...");
            Console.ReadKey();
        }
        private static void AddSamurais(params string[] names)
        {
            foreach (string name in names)
            {
                _context.Samurais.Add(new Samurai { Name = name });
            }
            _context.SaveChanges();
        }
        private static void AddVariousTypes()
        {
            _context.AddRange(new Samurai { Name = "Marcel" }, new Battle { Name = "Mall Băneasa" });
            _context.SaveChanges();
        }
        private static void GetSamurais(string text)
        {
            List<Samurai> samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (Samurai samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
        private static void InsertNewSamuraiWithAQuote(string quote, string samuraiName)
        {
            Samurai samurai = new Samurai
            {
                Name = samuraiName,
                Quotes = new List<Quote>
                {
                    new Quote { Text = quote}
                },
                Horse = new Horse
                {
                    Name = "Dilo"
                }

            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void getSamuraiDetails()
        {
            var samuraisWithQuotes = _context.Samurais.Include(s => s.Quotes).Include(s => s.Horse).ToList();
            foreach (Samurai samurai in samuraisWithQuotes)
            {
                Console.WriteLine(samurai.Id + samurai.Name + (samurai.Horse.Name != null ? samurai.Horse.Name : ""));
                foreach (Quote quote in samurai.Quotes)
                {
                    Console.WriteLine(quote.Id + quote.SamuraiId + quote.Text);
                }
            }
        }
    }
}
