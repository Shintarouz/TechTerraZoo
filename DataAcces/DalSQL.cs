using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTerra.Models;
using System.IO;

namespace TechTerra.DataAcces
{
    class DalSQL
    {
        //Eigenschappen
        private string serverName;
        private string databaseName;
        private string connectionString;

        //Constructor
        public DalSQL()
        {
            string path = Path.Combine("DataAcces", "DBConfig.txt");
            string[] lines = File.ReadAllLines(path);

            string userId = lines[0];
            string password = lines[1];

            serverName = "techterra-de-planten.database.windows.net";
            databaseName = "Techterra_De_Planten";
            connectionString =
                $"Server={serverName};" +
                $"Database={databaseName};" +
                $"User Id={userId};" +
                $"Password={password};" +
                $"Encrypt=True;" +
                $"TrustServerCertificate=False;";
        }


        // Haalt alle dieren op in de database en maakt er objecten van.
        public List<Dier> GetAllDieren()
        {
            string query = @"SELECT DierID, Naam, Soort, Leeftijd, VerblijfID, VerzorgerID FROM Dier";
            List<Dier> dieren = new List<Dier>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dierID = reader.GetString(0);
                        string naam = reader.GetString(1);
                        string soort = reader.GetString(2);
                        int leeftijd = reader.GetInt32(3);


                        Verblijf verblijf = null; // placeholder, vervang dit later
                        Verzorger verzorger = null; // placeholder, vervang dit later
                        // Eerst verblijven ophalen en daarna verzorger, pas als laatste dieren.

                        Dier dier = new Dier(dierID, naam, soort, leeftijd, null, null);
                        dieren.Add(dier);
                    }
                }
            }
            return dieren;
        }

        // Haalt alle verblijven op in de database en maakt er objecten van.
        public List<Verblijf> GetAllVerblijven()
        {
            string query = @"SELECT VerblijfID, Naam, Temperatuur, Capaciteit, TypeOmgeving FROM Verblijf";
            List<Verblijf> verblijven = new List<Verblijf>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string verblijfID = reader.GetString(0);
                        string naam = reader.GetString(1);
                        int temperatuur = reader.GetInt32(2);
                        int capaciteit = reader.GetInt32(3);
                        string typeOmgeving = reader.GetString(4);
                        Verblijf verblijf = new Verblijf(verblijfID, naam, temperatuur, capaciteit, typeOmgeving);
                        verblijven.Add(verblijf);
                    }
                }

            }
            return verblijven;
        }

        // Haalt alle verzorgers op in de database en maakt er objecten van.
        public List<Verzorger> GetAllVerzorgers()
        {
            string query = @"SELECT VerzorgerID, Naam FROM Verzorger";
            List<Verzorger> verzorgers = new List<Verzorger>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string verzorgerID = reader.GetString(0);
                        string naam = reader.GetString(1);
                        Verzorger verzorger = new Verzorger(naam, verzorgerID);
                        verzorgers.Add(verzorger);
                    }
                }
            }
            return verzorgers;
        }


        //public List<DierVoer> GetAllDierVoer()
        //{
        //    string query = @"SELECT VoerID, Naam, Hoeveelheid FROM DierVoer";
        //    List<DierVoer> dierVoerLijst = new List<DierVoer>();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        connection.Open();
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                string voerID = reader.GetString(0);
        //                string naam = reader.GetString(1);
        //                int hoeveelheid = reader.GetInt32(2);
        //                DierVoer dierVoer = new DierVoer(voerID, naam, hoeveelheid);
        //                dierVoerLijst.Add(dierVoer);
        //            }
        //        }
        //    }
        //    return dierVoerLijst;
        //}
    }

}
