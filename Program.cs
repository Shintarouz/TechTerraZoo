using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTerra.DataAcces;
using TechTerra.Models;

namespace TechTerra
{
    class Program
    {
        static List<Dier> dieren;
        static List<Verblijf> verblijven;
        static List<Verzorger> verzorgers;

        static void Main(string[] args)
        {
            CreateAllData();
            Running(); // start het menu
        }

        public static void Running()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("1. Dieren");
                Console.WriteLine("2. Voerschema");
                Console.WriteLine("3. Verblijven");
                Console.WriteLine("4. Verzorgers");
                Console.WriteLine("5. Afsluiten");
                Console.Write("Keuze : ");
                string inputChoice = Convert.ToString(Console.ReadLine());
                switch (inputChoice)
                {
                    case "1":
                        // Optie 1: Overzicht menu
                        DierMenu();
                        break;

                    case "2":
                        // Optie 2 : voerschema
                        VoerSchemaMenu();
                        break;

                    case "3":
                        // Optie 3 : verblijven
                        VerblijfMenu();
                        break;

                    case "4":
                        // Optie 4 : verzorgers
                        VerzorgerMenu();
                        break;

                    case "5":
                        // Optie 5 : Programma afsluiten
                        Console.WriteLine("Programma sluit af.");
                        running = false;
                        break;
                }
            }
        }

        public static void DierMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("1. Dier Overzicht");
                Console.WriteLine("2. Dier Toevoegen");
                Console.WriteLine("3. Dier Verwijderen");
                Console.WriteLine("4. Dier Toevoegen aan Verblijf");
                Console.WriteLine("5. Terug");
                Console.Write("Keuze : ");
                string inputChoice = Convert.ToString(Console.ReadLine());
                switch (inputChoice)
                {
                    case "1":
                        // Optie 1.1 : overzicht van dieren in de lijst.
                        Console.Clear();
                        if (dieren.Count == 0 )
                        {
                            Console.WriteLine("Er zijn geen dieren in de lijst.");
                        }
                        else
                        {
                            foreach (Dier dier in dieren)
                            {
                                Console.WriteLine(dier.ToString());
                            }
                        }
                        Console.ReadKey();
                        break;

                    case "2":
                        // Optie 1.2 : Nieuw dier toevoegen
                        Console.Clear();

                        Console.WriteLine("Nieuw dier toevoegen");

                        Console.Write("DierID :");
                        string id = Console.ReadLine();

                        Console.Write("Naam : ");
                        string naam = Console.ReadLine();

                        Console.Write("Soort : ");
                        string soort = Console.ReadLine();

                        Console.Write("Leeftijd : ");
                        int leeftijd = Convert.ToInt32(Console.ReadLine());

                        Dier nieuwDier = new Dier(id, naam, soort, leeftijd, null, null);
                        dieren.Add(nieuwDier);
                        // place holders, hier moeten verblijf en verzorger nog toegevoegd worden.
                        break;

                    case "3":
                        // Optie 1.3 : Dier verwijderen
                        Console.Clear();
                        if (dieren.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen dieren om te verwijderen.");
                            break;
                        }

                        Console.WriteLine("Kies een dier om te verwijderen:");

                        for (int i = 0; i < dieren.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {dieren[i].naam} ({dieren[i].soort})");
                        }
                        Console.Write("Nummer van dier: ");

                        if (!int.TryParse(Console.ReadLine(), out int keuze) || keuze < 1 || keuze > dieren.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            break;
                        }

                        Dier verwijderDier = dieren[keuze - 1];
                        dieren.RemoveAt(keuze - 1);


                        Console.WriteLine($"Dier {verwijderDier.naam} succesvol verwijderd.");
                        Console.ReadKey();
                        break;

                    case "4":
                        // Optie 1.4 : Dier toevoegen aan verblijf

                        // Nieuwe Lijst aanmaken met dieren, en deze vullen met dieren die niet in een verblijf zitten.
                        Console.Clear();
                        List<Dier> beschikbareDieren = new List<Dier>();
                        foreach (Dier dier in dieren)
                        {
                            bool zitInVerblijf = false;

                            foreach (Verblijf verblijf in verblijven)
                            {
                                if (verblijf.BevatDier(dier))
                                {
                                    zitInVerblijf = true;
                                    break;
                                }
                            }    
                            if (!zitInVerblijf)
                            {
                                beschikbareDieren.Add(dier);
                            }
                        }

                        // Foutmelding opvangen als er geen beschikbare dieren zijn.
                        if (beschikbareDieren.Count == 0)
                        {
                            Console.WriteLine("Alle dieren zitten al in een verblijf.");
                            Console.ReadKey();
                            break;
                        }

                        // Alle beschikbare dieren printen.
                        for (int i = 0; i < beschikbareDieren.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {beschikbareDieren[i].naam} ({beschikbareDieren[i].soort})");
                        }

                        // Foutmelding verwerking bij ongeldige keuze.
                        if (!int.TryParse(Console.ReadLine(), out int keuze2) || keuze2 < 1 || keuze2 > dieren.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            break;
                        }

                        // Geselecteerde dier assignen
                        Dier toevoegDier = beschikbareDieren[keuze2 - 1];

                        // Foutmelding opvangen bij geen verblijven.
                        if (verblijven.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen verblijven beschikbaar.");
                            break;
                        }

                        // 
                        for (int i = 0; i < verblijven.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {verblijven[i].naam}");
                        }

                        Console.WriteLine("Nummer van verblijf: ");

                        if (!int.TryParse(Console.ReadLine(), out int keuze3) || keuze3 < 1 || keuze3 > verblijven.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            break;
                        }

                        Verblijf gekozenVerblijf = verblijven[keuze3 - 1];
                        gekozenVerblijf.VoegDierToe(toevoegDier);
                        Console.WriteLine($"Dier {toevoegDier.naam} succesvol toegevoegd aan verblijf {gekozenVerblijf.naam}.");
                        Console.ReadKey();
                        break;

                    case "5":
                        // Optie 1.5 : Terug naar hoofdmenu
                        running = false;
                        break;
                }

            }
        }

        public static void VoerSchemaMenu()
        {

        }

        public static void VerblijfMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("1. Verblijf Overzicht");
                Console.WriteLine("2. Verblijf Toevoegen");
                Console.WriteLine("3. Verblijf Verwijderen");
                Console.WriteLine("4. Terug");
                Console.Write("Keuze : ");
                string inputChoice = Convert.ToString(Console.ReadLine());
                switch (inputChoice)
                {
                    case "1":
                        // Optie 3.1 : Verblijf overzicht
                        Console.Clear();
                        if (verblijven.Count == 0)
						{
							Console.WriteLine("Er zijn geen verblijven in de lijst.");
						}

						else
						{
							foreach (Verblijf verblijf in verblijven)
							{
								Console.WriteLine(verblijf.ToString());
							}
						}
                        Console.ReadKey();
                        break;
                    case "2":
						// Optie 3.2 : Verblijf toevoegen
						Console.Clear();

						Console.WriteLine("Nieuw verblijf toevoegen");

						Console.Write("VerblijfID :");
						string verblijfID = Console.ReadLine();

						Console.Write("Naam : ");
						string naam = Console.ReadLine();

						Console.Write("Temperatuur : ");
						decimal temperatuur = Convert.ToDecimal(Console.ReadLine());

						Console.Write("Capaciteit : ");
						int capaciteit = Convert.ToInt32(Console.ReadLine());

						Console.Write("Type omgeving : ");
						string typeOmgeving = Console.ReadLine();

						Verblijf nieuwVerblijf = new Verblijf(verblijfID, naam, temperatuur, capaciteit, typeOmgeving);
						verblijven.Add(nieuwVerblijf);
						// place holders, hier moeten verblijf en verzorger nog toegevoegd worden.
						break;
                    case "3":
						// Optie 3.3 : Verblijf verwijderen
						Console.Clear();

						if (verblijven.Count == 0)
						{
							Console.WriteLine("Er zijn geen verblijven om te verwijderen.");
							break;
						}

						Console.WriteLine("Kies een dier om te verwijderen:");

						for (int i = 0; i < verblijven.Count; i++)
						{
							Console.WriteLine($"{i + 1}. {verblijven[i].naam}");
						}
						Console.Write("Nummer van dier: ");

						if (!int.TryParse(Console.ReadLine(), out int keuze) || keuze < 1 || keuze > verblijven.Count)
						{
							Console.WriteLine("Ongeldige keuze.");
							break;
						}

						Verblijf verwijderVerblijven = verblijven[keuze - 1];
						verblijven.RemoveAt(keuze - 1);


						Console.WriteLine($"Verblijf {verwijderVerblijven.naam} succesvol verwijderd.");
						Console.ReadKey();
						break;
                    case "4":
                        // Optie 3.4 : Terug naar hoofdmenu
                        running = false;
                        break;
                }
            }
        }

        public static void VerzorgerMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("1. Verzorger Overzicht");
                Console.WriteLine("2. Verzorger Toevoegen");
                Console.WriteLine("3. Verzorger Verwijderen");
                Console.WriteLine("4. Terug");
                Console.Write("Keuze : ");
                string inputChoice = Convert.ToString(Console.ReadLine());
                switch (inputChoice)
                {
                    case "1":
                        // Optie 4.1 Verzorger overzicht
                        break;
                    case "2":
                        // Optie 4.2 Verzoger toevoegen
                        break;
                    case "3":
                        // Optie 4.3 Verzorger verwijderen
                        break;
                        // Optie 4.4 Terug naar hoofdmenu
                    case "4":
                        running = false;
                        break;
                }
            }

        }
        //public static void Running()
        //{
        //    // maak temp verblijf en verzorger aan
        //    Verblijf WachtVerblijf = new Verblijf("VW01", "WachtVerblijf", 20, 5, "Oerwoud");
        //    Dier inktvis = new Dier("D001", "Inky", "Inktvis", 3, WachtVerblijf, null);

        //    WachtVerblijf.VoegDierToe(inktvis);

        //    Verzorger WachtVerzorger = new Verzorger("WachtVerzorger", "WVZ01");

        //    // maak eerste verblijf en dier aan
        //    Verblijf TestVerblijf1 = new Verblijf("VW02", "TestVerblijf1", 20, 5, "Oerwoud");
        //    Verzorger Bart = new Verzorger("Bart", "VZ01");
        //    //TestVerblijf1.VoegDierToe(new Dier("12", "leo", "Leeuw", 2, WachtVerblijf, WachtVerzorger));

        //    List<Verblijf> verblijven = new List<Verblijf>();
        //    verblijven.Add(TestVerblijf1);


        //    // begin menu
        //    bool running = true;
        //    while (running)
        //    {
        //        Console.WriteLine("Keuze : ");
        //        Console.WriteLine("1. Dieren Overzicht");
        //        Console.WriteLine("2. Nieuw Dier Toevoegen");
        //        Console.WriteLine("3. Voerschema");
        //        Console.WriteLine("4. Verblijven");
        //        Console.WriteLine("5. Afsluiten");
        //        string inputChoice = Convert.ToString(Console.ReadLine());
        //        switch (inputChoice)
        //        {
        //            case "1":
        //                // Optie 1: Overzicht menu
        //                bool overzichtBool = true;
        //                while (overzichtBool)
        //                {
        //                    Console.WriteLine("Keuze : ");
        //                    Console.WriteLine("1. Dieren");
        //                    Console.WriteLine("2. Terug");
        //                    string inputChoice1 = Convert.ToString(Console.ReadLine());
        //                    switch (inputChoice1)
        //                    {
        //                        case "1":
        //                            // Optie 1.1 : overzicht van dieren in de lijst.
        //                            Console.Clear();
        //                            if (dieren.Count == 0 )
        //                            {
        //                                Console.WriteLine("Er zijn geen dieren in de lijst.");
        //                            }
        //                            else
        //                            {
        //                                foreach (Dier dier in dieren)
        //                                {
        //                                    Console.WriteLine(dier.ToString());
        //                                }
        //                            }
        //                            break;
        //                        case "2":
        //                            // Optie 1.2 : terug naar hoofdmenu
        //                            overzichtBool = false;
        //                            break;
        //                    }
        //                }
        //                break;
        //            case "2":
        //                // Optie 2 : nieuw dier toevoegen
        //                bool dierToevoegenBool = true;
        //                while (dierToevoegenBool)
        //                {
        //                    Console.WriteLine("Keuze : ");
        //                    Console.WriteLine("1. Dier Toevoegen");
        //                    Console.WriteLine("2. Dier Verwijderen");
        //                    Console.WriteLine("3. Terug");
        //                    string inputChoice2 = Convert.ToString(Console.ReadLine());
        //                    switch (inputChoice2)
        //                    {
        //                        case "1":
        //                            // Optie 2.1 : Nieuw dier toevoegen
        //                            Console.WriteLine("DierID :");
        //                            string ID = Convert.ToString(Console.ReadLine());

        //                            Console.WriteLine("Naam : ");
        //                            string naam = Convert.ToString(Console.ReadLine());

        //                            Console.WriteLine("Soort : ");
        //                            string soort = Convert.ToString(Console.ReadLine());

        //                            Console.WriteLine("Leeftijd : ");
        //                            int leeftijd = Convert.ToInt32(Console.ReadLine());

        //                            TestVerblijf1.VoegDierToe(new Dier(ID, naam, soort, leeftijd, WachtVerblijf, WachtVerzorger ));
        //                            Console.WriteLine(TestVerblijf1.ToString());
        //                            dierToevoegenBool = false;
        //                            break;
        //                        case "2":
        //                            Console.Clear();
        //                            Console.WriteLine("Kies een dier om te verwijderen:");

        //                            TestVerblijf1.ToonDieren();

        //                            Console.Write("Nummer van dier: ");
        //                            if (int.TryParse(Console.ReadLine(), out int keuze))
        //                            {
        //                                if (TestVerblijf1.VerwijderDierOpIndex(keuze - 1))
        //                                {
        //                                    Console.WriteLine("Dier succesvol verwijderd.");
        //                                }
        //                                else
        //                                {
        //                                    Console.WriteLine("Ongeldige keuze.");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Console.WriteLine("Voer een geldig nummer in.");
        //                            }

        //                            Console.WriteLine("Druk op een toets om door te gaan...");
        //                            Console.ReadKey();
        //                            break;
        //                        case "3":
        //                            // Optie 2.3 : terug naar hoofdmenu
        //                            Console.WriteLine("Terug naar hoofdmenu");
        //                            dierToevoegenBool = false;
        //                            break;

        //                    }
        //                }
        //                break;
        //            case "3":
        //                // Optie 3 : voerschema
        //                break;
        //            case "4":
        //                // Optie 4 : verblijven
        //                bool verblijfMenu = true;
        //                while ( verblijfMenu)
        //                {
        //                    Console.Clear();
        //                    Console.WriteLine("Keuze : ");
        //                    Console.WriteLine("1. Verblijven toevoegen");
        //                    Console.WriteLine("2. Verblijven verwijderen");
        //                    Console.WriteLine("3. Terug");

        //                    string verblijfKeuze = Console.ReadLine();

        //                    switch (verblijfKeuze)
        //                    {
        //                        case "1":
        //                            // Optie 4.1 : Verblijf toevoegen
        //                            Console.Clear();
        //                            Console.WriteLine("Nieuw verblijf aanmaken");

        //                            Console.WriteLine("Verblijf ID : ");
        //                            string verblijfID = Convert.ToString(Console.ReadLine());

        //                            Console.WriteLine("Verblijf Naam : ");
        //                            string verblijfNaam = Convert.ToString(Console.ReadLine());


        //                            Console.WriteLine("Temperatuur : ");
        //                            int temperatuur = Convert.ToInt32(Console.ReadLine());

        //                            Console.WriteLine("Capaciteit : ");
        //                            int capaciteit = Convert.ToInt32(Console.ReadLine());

        //                            Console.WriteLine("Type Omgeving : ");
        //                            string typeOmgeving = Convert.ToString(Console.ReadLine());

        //                            Verblijf nieuwVerblijf = new Verblijf(verblijfID, verblijfNaam, temperatuur, capaciteit, typeOmgeving);
        //                            verblijven.Add(nieuwVerblijf);

        //                            Console.WriteLine("Verblijf succesvol toegevoegd. Druk op een toets om door te gaan...");
        //                            Console.ReadKey();
        //                            break;
        //                        case "2":
        //                            // Optie 4.2 : Verblijf verwijderen
        //                            Console.WriteLine("Verblijf verwijderen nog niet geïmplementeerd.");
        //                            break;
        //                        case "3":
        //                            // Optie 4.3 : terug naar hoofdmenu
        //                            verblijfMenu = false;
        //                            break;
        //                    }
        //                }
        //                break;
        //            case "5":
        //                // Optie 5 : afsluiten
        //                Console.WriteLine("Programma sluit af.");
        //                running = false;
        //                break;
        //        }
        //    }
        //}

        public static void CreateAllData()
        {
            DalSQL dalsql = new DalSQL();

            // assign data aan lijsten.
            dieren = dalsql.GetAllDieren();
            verblijven = dalsql.GetAllVerblijven();
            verzorgers = dalsql.GetAllVerzorgers();


            // mag later verwijdert worden, Alleen bedoelt voor snelle print functies
            foreach (Dier dier in dieren)
            {
                Console.WriteLine(dier.ToString());
            }

            foreach (Verblijf verblijf in verblijven)
            {
                Console.WriteLine(verblijf.ToString());
            }

            foreach (Verzorger verzorger in verzorgers)
            {
                Console.WriteLine(verzorger.ToString());
            }
        }

        //public static void CreateVerblijfData()
        //{
        //    DalSQL dalsql = new DalSQL();
        //    var verblijven = dalsql.GetAllVerblijven();
        //    foreach (var verblijf in verblijven)
        //    {
        //        Console.WriteLine($"Id: {verblijf.verblijfID}, Naam: {verblijf.naam}, AantalDieren: {verblijf.AantalDieren()}"); // VERANDER DIT LATER VANWEGE PROTECTION LEVEL!!!
        //    }
        //}

        //public static void CreateVerzorgerData()
        //{
        //    DalSQL dalsql = new DalSQL();
        //    var verzorgers = dalsql.GetAllVerzorgers();
        //    foreach (var verzorger in verzorgers)
        //    {
        //        Console.WriteLine($"Id: {verzorger.verzorgerID}, Naam: {verzorger.naam}"); // VERANDER DIT LATER VANWEGE PROTECTION LEVEL!!!
        //    }
        //}
    }
}
