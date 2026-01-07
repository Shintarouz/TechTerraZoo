using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCode.DataAcces;
using TemplateCode.Models;

namespace TemplateCode
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDierData(); // Zet SQL data klaar
            Running(); // start het menu
        }
        public static void Running()
        {   
            // maak temp verblijf en verzorger aan
            Verblijf WachtVerblijf = new Verblijf("WachtVerblijf", "WV01", 0, 20, 10, "Algemeen");
            Verzorger WachtVerzorger = new Verzorger("WachtVerzorger", "WVZ01");

            // maak eerste verblijf en dier aan
            Verblijf TestVerblijf1 = new Verblijf("Verblijf01", "VB01", 0, 18, 5, "Tropisch");
            Verzorger Bart = new Verzorger("Bart", "VZ01");
            //TestVerblijf1.VoegDierToe(new Dier("12", "leo", "Leeuw", 2, WachtVerblijf, WachtVerzorger));

            List<Verblijf> verblijven = new List<Verblijf>();
            verblijven.Add(TestVerblijf1);


            // begin menu
            bool running = true;
            while (running)
            {
                Console.WriteLine("Keuze : ");
                Console.WriteLine("1. Dieren Overzicht");
                Console.WriteLine("2. Nieuw Dier Toevoegen");
                Console.WriteLine("3. Voerschema");
                Console.WriteLine("4. Verblijven");
                Console.WriteLine("5. Afsluiten");
                string inputChoice = Convert.ToString(Console.ReadLine());
                switch (inputChoice)
                {
                    case "1":
                        // Optie 1: Overzicht menu
                        bool overzichtBool = true;
                        while (overzichtBool)
                        {
                            Console.WriteLine("Keuze : ");
                            Console.WriteLine("1. Dieren");
                            Console.WriteLine("2. Terug");
                            string inputChoice1 = Convert.ToString(Console.ReadLine());
                            switch (inputChoice1)
                            {
                                case "1":
                                    // Optie 1.1 : overzicht van dieren in de lijst.
                                    Console.Clear();
                                    Console.WriteLine(TestVerblijf1.ToString());
                                    break;
                                case "2":
                                    // Optie 1.2 : terug naar hoofdmenu
                                    overzichtBool = false;
                                    break;
                            }
                        }
                        break;
                    case "2":
                        // Optie 2 : nieuw dier toevoegen
                        bool dierToevoegenBool = true;
                        while (dierToevoegenBool)
                        {
                            Console.WriteLine("Keuze : ");
                            Console.WriteLine("1. Dier Toevoegen");
                            Console.WriteLine("2. Dier Verwijderen");
                            Console.WriteLine("3. Terug");
                            string inputChoice2 = Convert.ToString(Console.ReadLine());
                            switch (inputChoice2)
                            {
                                case "1":
                                    // Optie 2.1 : Nieuw dier toevoegen
                                    Console.WriteLine("DierID :");
                                    string ID = Convert.ToString(Console.ReadLine());

                                    Console.WriteLine("Naam : ");
                                    string naam = Convert.ToString(Console.ReadLine());

                                    Console.WriteLine("Soort : ");
                                    string soort = Convert.ToString(Console.ReadLine());

                                    Console.WriteLine("Leeftijd : ");
                                    int leeftijd = Convert.ToInt32(Console.ReadLine());

                                    TestVerblijf1.VoegDierToe(new Dier(ID, naam, soort, leeftijd, WachtVerblijf, WachtVerzorger ));
                                    Console.WriteLine(TestVerblijf1.ToString());
                                    dierToevoegenBool = false;
                                    break;
                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Kies een dier om te verwijderen:");

                                    TestVerblijf1.ToonDieren();

                                    Console.Write("Nummer van dier: ");
                                    if (int.TryParse(Console.ReadLine(), out int keuze))
                                    {
                                        if (TestVerblijf1.VerwijderDierOpIndex(keuze - 1))
                                        {
                                            Console.WriteLine("Dier succesvol verwijderd.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ongeldige keuze.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Voer een geldig nummer in.");
                                    }

                                    Console.WriteLine("Druk op een toets om door te gaan...");
                                    Console.ReadKey();
                                    break;
                                case "3":
                                    // Optie 2.3 : terug naar hoofdmenu
                                    Console.WriteLine("Terug naar hoofdmenu");
                                    dierToevoegenBool = false;
                                    break;

                            }
                        }
                        break;
                    case "3":
                        // Optie 3 : voerschema
                        break;
                    case "4":
                        // Optie 4 : verblijven
                        bool verblijfMenu = true;
                        while ( verblijfMenu)
                        {
                            Console.Clear();
                            Console.WriteLine("Keuze : ");
                            Console.WriteLine("1. Verblijven toevoegen");
                            Console.WriteLine("2. Verblijven verwijderen");
                            Console.WriteLine("3. Terug");

                            string verblijfKeuze = Console.ReadLine();

                            switch (verblijfKeuze)
                            {
                                case "1":
                                    // Optie 4.1 : Verblijf toevoegen
                                    Console.Clear();
                                    Console.WriteLine("Nieuw verblijf aanmaken");

                                    Console.WriteLine("Verblijf Naam : ");
                                    string verblijfNaam = Convert.ToString(Console.ReadLine());

                                    Console.WriteLine("Verblijf ID : ");
                                    string verblijfID = Convert.ToString(Console.ReadLine());

                                    Console.WriteLine("Temperatuur : ");
                                    int temperatuur = Convert.ToInt32(Console.ReadLine());

                                    Console.WriteLine("Capaciteit : ");
                                    int capaciteit = Convert.ToInt32(Console.ReadLine());

                                    Console.WriteLine("Type Omgeving : ");
                                    string typeOmgeving = Convert.ToString(Console.ReadLine());

                                    Verblijf nieuwVerblijf = new Verblijf(verblijfNaam, verblijfID, 0, temperatuur, capaciteit, typeOmgeving);
                                    verblijven.Add(nieuwVerblijf);

                                    Console.WriteLine("Verblijf succesvol toegevoegd. Druk op een toets om door te gaan...");
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    // Optie 4.2 : Verblijf verwijderen
                                    Console.WriteLine("Verblijf verwijderen nog niet geïmplementeerd.");
                                    break;
                                case "3":
                                    // Optie 4.3 : terug naar hoofdmenu
                                    verblijfMenu = false;
                                    break;
                            }
                        }
                        break;
                    case "5":
                        // Optie 5 : afsluiten
                        Console.WriteLine("Programma sluit af.");
                        running = false;
                        break;
                }
            }
        }
        public static void CreateDierData()
        {
            Console.WriteLine("Uitgevoerd");
            DalSQL dalsql = new DalSQL();
            var dieren = dalsql.GetAllDieren();

            foreach (var dier in dieren)
            {
                Console.WriteLine($"Id: {dier.dierID}, Naam: {dier.naam}, Soort: {dier.soort}, Leeftijd: {dier.leeftijd}");
            }

            //List<Product> producten = dalsql.GetAllProducts();
            //foreach (var product in producten)
            //{
            //    Console.WriteLine($"Id: {product.Id}, Naam: {product.Name}, Prijs: {product.Price}");
            //}
        }
    }
}
