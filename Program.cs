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
                PrintFunctie("Hoofdmenu");
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
                        // Optie 1: Overzicht Menu
                        DierMenu();
                        break;

                    case "2":
                        // Optie 2 : Voerschema Menu
                        VoerSchemaMenu();
                        break;

                    case "3":
                        // Optie 3 : Verblijven Menu
                        VerblijfMenu();
                        break;

                    case "4":
                        // Optie 4 : Verzorgers Menu
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
                PrintFunctie("Dier Menu");
                Console.WriteLine("1. Dier Overzicht");
                Console.WriteLine("2. Dier Toevoegen");
                Console.WriteLine("3. Dier Verwijderen");
                Console.WriteLine("4. Dier Toevoegen aan Verblijf");
                Console.WriteLine("5. Dier Toevoegen aan Verzorger");
                Console.WriteLine("6. Dier Zoeken");
                Console.WriteLine("7. Dier verplaatsen");
                Console.WriteLine("8. Terug naar hoofdmenu");
                Console.Write("Keuze : ");

                string inputChoice = Convert.ToString(Console.ReadLine());
                switch (inputChoice)
                {
                    case "1":
                        // Optie 1.1 : overzicht van dieren in de lijst.
                        Console.Clear();
                        PrintFunctie("Dier Overzicht");

                        // Controleren of er dieren zijn.
                        if (dieren.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen dieren in de lijst.");
                        }
                        else // Anders toon alle dieren.
                        {
                            foreach (Dier dier in dieren)
                            {
                                Console.WriteLine(dier.ToString());
                            }
                        }
                        Console.WriteLine("Klik een toets om verder te gaan...");
                        Console.ReadKey();
                        break;

                    case "2":
                        // Optie 1.2 : Nieuw dier toevoegen
                        Console.Clear();
                        PrintFunctie("Dier Toevoegen");
                        Console.WriteLine("Nieuw dier toevoegen");

                        // Invoer gegevens van nieuw dier invoeren en 
                        // converten in het geval van andere datatypen.
                        string id = GenereerDierID();


                        Console.Write("Naam : ");
                        string naam = Convert.ToString(Console.ReadLine());

                        Console.Write("Soort : ");
                        string soort = Convert.ToString(Console.ReadLine());

                        Console.Write("Leeftijd : ");
                        int leeftijd = Convert.ToInt32(Console.ReadLine());

                        // Controleren of er verblijven zijn.
                        if (verblijven.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen verblijven beschikbaar.");
                            break;
                        }

                        // Verblijven tonen.
                        for (int i = 0; i < verblijven.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {verblijven[i].naam}");
                        }

                        // Verblijfkeuze kiezen en valideren.
                        Console.Write("Kies een nummer van verblijf: ");
                        if (!int.TryParse(Console.ReadLine(), out int verblijfKeuze) || verblijfKeuze < 1 || verblijfKeuze > verblijven.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            break;
                        }

                        // Verblijf assignen.
                        Verblijf keuzeVerblijf = verblijven[verblijfKeuze - 1];

                        // Controleren of er verzorgers zijn.
                        if (verzorgers.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen verzorgers beschikbaar.");
                            break;
                        }

                        // Verzorgers tonen.
                        for (int i = 0; i < verzorgers.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {verzorgers[i].naam}");
                        }

                        // Verzorgerkeuze kiezen en valideren.
                        Console.Write("Kies een nummer van verzorger : ");
                        if (!int.TryParse(Console.ReadLine(), out int verzorgerKeuze) || verzorgerKeuze < 1 || verzorgerKeuze > verblijven.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            break;
                        }

                        // Verzorger assignen.
                        Verzorger keuzeVerzorger = verzorgers[verzorgerKeuze - 1];

                        // Nieuw dier aanmaken met de ingevulde waardes en toevoegen aan dieren lijst.
                        Dier nieuwDier = new Dier(id, naam, soort, leeftijd, keuzeVerblijf, keuzeVerzorger);
                        dieren.Add(nieuwDier);

                        // Verblijf en Verzorger in de code koppelen aan het dier.
                        keuzeVerblijf.VoegDierToe(nieuwDier);
                        keuzeVerzorger.VoegDierToe(nieuwDier);

                        // Nieuw dier aanmaken in de database.
                        DalSQL.DBAddDier(nieuwDier);
                        break;

                    case "3":
                        // Optie 1.3 : Dier verwijderen
                        Console.Clear();
                        PrintFunctie("Dier Verwijderen");
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
                        PrintFunctie("Dier Toevoegen aan Verblijf");
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
                        // Optie 1.5 : Dier toevoegen aan Verzorger
                        Console.Clear();
                        PrintFunctie("Dier Toevoegen aan Verzorger");
                        if (dieren.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen dieren om te kiezen.");
                            break;
                        }

                        Console.WriteLine("Kies een dier :");

                        for (int i = 0; i < dieren.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {dieren[i].naam} ({dieren[i].soort})");
                        }

                        Console.Write("Nummer van dier : ");

                        if (!int.TryParse(Console.ReadLine(), out int dierToevoegKeuze) || dierToevoegKeuze < 1 || dierToevoegKeuze > dieren.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            break;
                        }

                        Dier verzorgToevoegDier = dieren[dierToevoegKeuze - 1];

                        if (verzorgers.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen verzorgers om te kiezen.");
                            break;
                        }

                        Console.WriteLine("Kies een verzorger :");

                        for (int i = 0; i < verzorgers.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {verzorgers[i].naam} ({verzorgers[i].verzorgerID})");
                        }

                        Console.WriteLine("Nummer van verzorger : ");

                        if (!int.TryParse(Console.ReadLine(), out int verzorgerToevoegKeuze) || verzorgerToevoegKeuze < 1 || verzorgerToevoegKeuze > dieren.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            break;
                        }

                        Verzorger gekozenVerzorger = verzorgers[verzorgerToevoegKeuze - 1];

                        gekozenVerzorger.VoegDierToe(verzorgToevoegDier);

                        Console.WriteLine($"Dier {verzorgToevoegDier.naam} succesvol toegevoegd aan Verzorger {gekozenVerzorger.naam}.");
                        Console.ReadKey();
                        break;
                    case "6":
                        // Optie 1.6 : Dier Zoeken
                        ZoekDier();
                        break;

                    case "7":
                        // Optie 1.7 : Dier verplaatsen
                        VerplaatsDier();
                        break;

                    case "8":
                        // Optie 1.8 : Terug naar hoofdmenu
                        running = false;
                        break;
                }

            }
        }

        public static void VoerSchemaMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                PrintFunctie("Voerschema Menu");
                Console.WriteLine("1. VoerSchema ( Niet geimplementeerd )");
                Console.WriteLine("2. Terug naar hoofdmenu");
                string inputChoice = Convert.ToString(Console.ReadLine());
                switch (inputChoice)
                {
                    case "1":
                        Console.WriteLine("Hier komt voerschema");
                        Console.ReadKey();
                        break;
                    case "2":
                        running = false;
                        break;
                }
            }
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
                Console.WriteLine("4. Terug naar hoofdmenu");
                Console.Write("Keuze : ");
                string inputChoice = Convert.ToString(Console.ReadLine());
                switch (inputChoice)
                {
                    case "1":
                        // Optie 3.1 : Verblijf overzicht
                        Console.Clear();
                        PrintFunctie("Verblijf Overzicht");
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
                        PrintFunctie("Verblijf Toevoegen");

                        Console.WriteLine("Nieuw verblijf toevoegen");

                        string verblijfID = GenereerVerblijfID();

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

                        DalSQL.DBAddVerblijf(nieuwVerblijf); // voegt verblijf to aan de database

                        break;
                    case "3":
                        // Optie 3.3 : Verblijf verwijderen
                        Console.Clear();
                        PrintFunctie("Verblijf Verwijderen");

                        if (verblijven.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen verblijven om te verwijderen.");
                            break;
                        }

                        Console.WriteLine("Kies een verblijf om te verwijderen:");

                        for (int i = 0; i < verblijven.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {verblijven[i].naam}");
                        }
                        Console.Write("Nummer van verblijf : ");

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
                Console.WriteLine("4. Terug naar hoofdmenu");
                Console.Write("Keuze : ");
                string inputChoice = Convert.ToString(Console.ReadLine());
                switch (inputChoice)
                {
                    case "1":
                        // Optie 4.1 Verzorger overzicht
                        Console.Clear();
                        PrintFunctie("Verzorger Overzicht");
                        if (verzorgers.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen verzorgers in de lijst.");
                        }

                        else
                        {
                            foreach (Verzorger verzorger in verzorgers)
                            {
                                Console.WriteLine(verzorger.ToString());
                            }
                        }
                        Console.ReadKey();
                        break;
                    case "2":
                        // Optie 4.2 Verzoger toevoegen
                        Console.Clear();
                        PrintFunctie("Verzorger Toevoegen");
                        Console.WriteLine("Nieuwe verzorger toevoegen");

                        string verzorgerID = GenereerVerzorgerID();


						Console.Write("Naam : ");
                        string naam = Console.ReadLine();

                        Verzorger nieuweVerzorger = new Verzorger(verzorgerID, naam);
                        verzorgers.Add(nieuweVerzorger);

                        DalSQL.DBAddVerzorger(nieuweVerzorger); // voegt verzorger to aan de database

                        break;
                    case "3":
                        // Optie 4.3 Verzorger verwijderen
                        Console.Clear();
                        PrintFunctie("Verzorger Verwijderen");

                        if (verzorgers.Count == 0)
                        {
                            Console.WriteLine("Er zijn geen verzorgers om te verwijderen.");
                            break;
                        }

                        Console.WriteLine("Kies een verzorger om te verwijderen:");

                        for (int i = 0; i < verzorgers.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {verzorgers[i].naam}");
                        }
                        Console.Write("Nummer van verzorger: ");

                        if (!int.TryParse(Console.ReadLine(), out int keuze) || keuze < 1 || keuze > verzorgers.Count)
                        {
                            Console.WriteLine("Ongeldige keuze.");
                            break;
                        }

                        Verzorger verwijderVerzorger = verzorgers[keuze - 1];
                        verzorgers.RemoveAt(keuze - 1);

                        Console.WriteLine($"Verzorger {verwijderVerzorger.naam} succesvol verwijderd.");
                        Console.WriteLine($"Verzorger {verwijderVerzorger} succesvol verwijderd.");
                        Console.ReadKey();
                        break;
                    // Optie 4.4 Terug naar hoofdmenu
                    case "4":
                        running = false;
                        break;
                }
            }

        }


        public static void ZoekDier()
        {
            Console.Clear();
            PrintFunctie("Dier Zoeken");

            if (dieren.Count == 0)
            {
                Console.WriteLine("Er zijn geen dieren om te zoeken.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("=== DIER ZOEKEN ===  ");
            Console.WriteLine("1. Zoeken op Naam");
            Console.WriteLine("2. Zoeken op Soort");
            Console.WriteLine("3. Terug");
            Console.Write("Keuze : ");

            string keuze = Console.ReadLine();

            switch (keuze)
            {
                case "1":
                    // Zoeken op naam
                    Console.Clear();
                    PrintFunctie("Zoeken op Naam");
                    Console.Write("Voer naam in (of deel van naam) : ");
                    string zoekNaam = Console.ReadLine().ToLower();

                    List<Dier> gevondenOpNaam = dieren
                        .Where(d => d.naam.ToLower().Contains(zoekNaam))
                        .ToList();

                    ToonZoekResultaten(gevondenOpNaam, "naam", zoekNaam);
                    break;

                case "2":
                    // Zoeken op soort
                    Console.Clear();
                    PrintFunctie("Zoeken op Soort");
                    Console.Write("Voer soort in (of deel van soort) : ");
                    string zoekSoort = Console.ReadLine().ToLower();

                    List<Dier> gevondenOpSoort = dieren
                        .Where(d => d.soort.ToLower().Contains(zoekSoort))
                        .ToList();

                    ToonZoekResultaten(gevondenOpSoort, "soort", zoekSoort);
                    break;

                case "3":
                    // Terug
                    break;

                default:
                    Console.WriteLine("Ongeldige keuze.");
                    Console.ReadKey();
                    break;
            }
        }

        private static void ToonZoekResultaten(List<Dier> resultaten, string zoekType, string zoekTerm)
        {
            Console.Clear();
            Console.WriteLine($"=== ZOEKRESULTATEN VOOR {zoekTerm} (zoeken op {zoekType}) ===\n");

            if (resultaten.Count == 0)
            {
                Console.WriteLine($"Geen dieren gevonden met {zoekType} {zoekTerm}.");
            }
            else
            {
                Console.WriteLine($"Gevonden: {resultaten.Count} dier(en)\n");

                foreach (Dier dier in resultaten)
                {
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine($"Naam      : {dier.naam}");
                    Console.WriteLine($"Soort     : {dier.soort}");
                    Console.WriteLine($"DierID    : {dier.dierID}");
                    Console.WriteLine($"Leeftijd  : {dier.leeftijd} jaar");

                    if (dier.inVerblijf != null)
                    {
                        Console.WriteLine($"Verblijf  : {dier.inVerblijf.naam}");
                    }

                    if (dier.verzorger != null)
                    {
                        Console.WriteLine($"Verzorger : {dier.verzorger.naam}");
                    }
                }
                Console.WriteLine("----------------------------------");
            }

            Console.WriteLine("\nDruk op een toets om terug te gaan...");
            Console.ReadKey();
        }

        public static string PrintFunctie(string invoer)
        {
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine($"{invoer}                               ");
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine();

            return "Dit is een test functie";
        }
        // OUDE CODE ---------------------------------
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
        // ----------------------------------------------------
        public static void CreateAllData()
        {
            DalSQL dalsql = new DalSQL();

            // assign data aan lijsten.
            verblijven = dalsql.GetAllVerblijven();
            verzorgers = dalsql.GetAllVerzorgers();
            dieren = dalsql.GetAllDieren();


            // mag later verwijdert worden, Alleen bedoelt voor snelle print functies
            //foreach (Dier dier in dieren)
            //{
            //    Console.WriteLine(dier.ToString());
            //}

            //foreach (Verblijf verblijf in verblijven)
            //{
            //    Console.WriteLine(verblijf.ToString());
            //}

            //foreach (Verzorger verzorger in verzorgers)
            //{
            //    Console.WriteLine(verzorger.ToString());
            //}
        }
        private static string GenereerDierID()
        {
            // Als er nog geen dieren zijn, begin bij 0001
            if (dieren.Count == 0)
            {
                return "0001";
            }

            // Vind het hoogste nummer uit alle bestaande dieren
            int hoogsteNummer = 0;

            foreach (Dier dier in dieren)
            {
                // Probeer de ID om te zetten naar een nummer
                if (int.TryParse(dier.dierID, out int nummer))
                {
                    if (nummer > hoogsteNummer)
                    {
                        hoogsteNummer = nummer;
                    }
                }
            }

            // Tel 1 bij het hoogste nummer op
            int nieuwNummer = hoogsteNummer + 1;

            // Maak een ID met 4 cijfers (0001, 0002, etc.)
            return nieuwNummer.ToString("D4");
        }

        private static string GenereerVerblijfID()
        {
            // Als er nog geen dieren zijn, begin bij 0001
            if (verblijven.Count == 0)
            {
                return "0001";
            }

            // Vind het hoogste nummer uit alle bestaande dieren
            int hoogsteNummer = 0;

            foreach (Verblijf verblijf in verblijven)
            {
                // Probeer de ID om te zetten naar een nummer
                if (int.TryParse(verblijf.verblijfID, out int nummer))
                {
                    if (nummer > hoogsteNummer)
                    {
                        hoogsteNummer = nummer;
                    }
                }
            }

            // Tel 1 bij het hoogste nummer op
            int nieuwNummer = hoogsteNummer + 1;

            // Maak een ID met 4 cijfers (0001, 0002, etc.)
            return nieuwNummer.ToString("D4");
        }
		private static string GenereerVerzorgerID()
		{
			// Als er nog geen dieren zijn, begin bij 0001
			if (verzorgers.Count == 0)
			{
				return "0001";
			}

			// Vind het hoogste nummer uit alle bestaande dieren
			int hoogsteNummer = 0;

			foreach (Verzorger verzorger in verzorgers)
			{
				// Probeer de ID om te zetten naar een nummer
				if (int.TryParse(verzorger.verzorgerID, out int nummer))
				{
					if (nummer > hoogsteNummer)
					{
						hoogsteNummer = nummer;
					}
				}
			}

			// Tel 1 bij het hoogste nummer op
			int nieuwNummer = hoogsteNummer + 1;

			// Maak een ID met 4 cijfers (0001, 0002, etc.)
			return nieuwNummer.ToString("D4");
		}

        public static void VerplaatsDier()
        {
            for (int i = 0; i < dieren.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dieren[i].naam} ({dieren[i].soort})");
            }
            Console.Write("Nummer van dier: ");

            if (!int.TryParse(Console.ReadLine(), out int keuze) || keuze < 1 || keuze > dieren.Count)
            {
                Console.WriteLine("Ongeldige keuze.");         
            }

            Dier VerplaatsDier = dieren[keuze -1];
            string VerblijfID = DalSQL.DBGetVerblijfID(VerplaatsDier.dierID);
            

            for (int i = 0; i < verblijven.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {verblijven[i].naam}");
            }
            Console.Write("Nummer van verblijf : ");

            if (!int.TryParse(Console.ReadLine(), out int keuze2) || keuze2 < 1 || keuze2 > verblijven.Count)
            {
                Console.WriteLine("Ongeldige keuze.");
            }

            Verblijf VerplaatsVerblijf = verblijven[keuze - 1];
            VerblijfID = VerplaatsVerblijf.verblijfID;

            DalSQL.UpdateVerblijfVanDier(VerplaatsDier.dierID, VerplaatsVerblijf.verblijfID);

            //string VerblijfID = DBGetVerblijfID("htgt");
        }
	}
}