using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTerra.Models;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace TechTerra.DataAcces
{
    class DalSQL
    {
        //Eigenschappen
        private string serverName;
        private string databaseName;
        private static string connectionString;

        //Constructor voor online server
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

        //// constructor voor lokale database, VERANDER DE WAARDES OM TE GEBRUIKEN!!
        //public DalSQL()
        //{
        //    serverName = "LAPTOP-5FM0T3FM";
        //    databaseName = "TestTerraZoo"; 
        //    connectionString = $"Server={serverName};Database={databaseName};Trusted_Connection = True; TrustServerCertificate = True; ";
        //}

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

                        string verblijfID = reader.GetString(4);
                        string verzorgerID = reader.GetString(5);

                        Verblijf verblijf = GetVerblijf(verblijfID); // placeholder, vervang dit later
                        Verzorger verzorger = GetVerzorger(verzorgerID); // placeholder, vervang dit later

                        Dier dier = new Dier(dierID, naam, soort, leeftijd, verblijf, verzorger);
                        dieren.Add(dier);
                    }
                }
            }
            return dieren;
        }
        public Verzorger GetVerzorger(string verzorgerID)
        {
            string query = "SELECT * FROM verzorger WHERE VerzorgerID = @VerzorgerID";
            Verzorger verzorger = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@VerzorgerID", verzorgerID);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string VerzorgerID = reader.GetString(0);
                        string naam = reader.GetString(1);
                        verzorger = new Verzorger(verzorgerID, naam);
                    }
                }
            }
            return verzorger;
        }
        public Verblijf GetVerblijf(string verblijfID)
        {
            string query = "SELECT * FROM verblijf WHERE VerblijfID = @verblijfID";
            Verblijf verblijf = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@VerblijfID", verblijfID);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string naam = reader.GetString(1);
                        int temperatuur = reader.GetInt32(2);
                        int capaciteit = reader.GetInt32(3);
                        string typeOmgeving = reader.GetString(4);
                        verblijf = new Verblijf(verblijfID, naam, temperatuur, capaciteit, typeOmgeving);
                    }
                }
            }
            return verblijf;
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
			// voeg dieren toe aan verblijven in code
			foreach (Verblijf verblijf in verblijven)
			{
				List<Dier> dierenInVerblijf = GetDierenVoorVerblijf(verblijf.verblijfID);
				foreach (Dier dier in dierenInVerblijf)
				{
					verblijf.VoegDierToe(dier);
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
                        Verzorger verzorger = new Verzorger(verzorgerID, naam);
                        verzorgers.Add(verzorger);
                    }
                }
            }
            return verzorgers;
        }

        // Dier toevoegen aan de database
        public static void DBAddDier(Dier dier)
        {
            string query = @"INSERT INTO Dier(DierID, Naam, Soort, Leeftijd, VerblijfID, VerzorgerID) 
                            VALUES(@DierID, @Naam, @Soort, @Leeftijd, @VerblijfID, @VerzorgerID)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DierID", dier.dierID);
                command.Parameters.AddWithValue("@Naam", dier.naam);
                command.Parameters.AddWithValue("@Soort", dier.soort);
                command.Parameters.AddWithValue("@Leeftijd", dier.leeftijd);

                command.Parameters.AddWithValue("@verblijfID", dier.inVerblijf.verblijfID);
                command.Parameters.AddWithValue("@verzorgerID", dier.verzorger.verzorgerID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Verzorger toevoegen aan de database
        public static void DBAddVerzorger(Verzorger verzorger)
        {
            // INSERT dierID weglaten bij invoer en databse auto laten generaten.
            string query = @"INSERT INTO Verzorger(verzorgerID, Naam)
                           VALUES(@verzorgerID, @Naam)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@verzorgerID", verzorger.verzorgerID);
                command.Parameters.AddWithValue("@Naam", verzorger.naam);

                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        // Verblijf toevoegen aan de database
        public static void DBAddVerblijf(Verblijf verblijf)
        {
            string query = @"INSERT INTO Verblijf(VerblijfID, Naam, Temperatuur, Capaciteit, TypeOmgeving)
                           VALUES(@VerblijfID, @Naam, @Temperatuur, @Capaciteit, @TypeOmgeving)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@VerblijfID", verblijf.verblijfID);
                command.Parameters.AddWithValue("@Naam", verblijf.naam);
                command.Parameters.AddWithValue("@Temperatuur", verblijf.temperatuur);
                command.Parameters.AddWithValue("@Capaciteit", verblijf.capaciteit);
                command.Parameters.AddWithValue("@TypeOmgeving", verblijf.typeOmgeving);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // haalt het VerblijfID op uit DierID
        public static string DBGetVerblijfID(string dierID)
        {
            string query = @"SELECT VerblijfID FROM Dier WHERE DierID = @DierID";
            string VerblijfID = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DierID", dierID);


                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VerblijfID = reader.GetString(0);
                    }
                }

            }
            return VerblijfID;
        }

        public static void UpdateVerblijfVanDier(string dierID, string nieuwVerblijfID)
        {
            string query = "UPDATE dier SET VerblijfID = @verblijfID WHERE DierID = @dierID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@verblijfID", nieuwVerblijfID);
                command.Parameters.AddWithValue("@dierID", dierID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Dier> GetDierenVoorVerblijf(string verblijfID)
        {
            string query = @"SELECT DierID, Naam, Soort, Leeftijd, VerblijfID, VerzorgerID 
                     FROM Dier 
                     WHERE VerblijfID = @VerblijfID";
            List<Dier> dieren = new List<Dier>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@VerblijfID", verblijfID);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dierID = reader.GetString(0);
                        string naam = reader.GetString(1);
                        string soort = reader.GetString(2);
                        int leeftijd = reader.GetInt32(3);
                        string verzorgerID = reader.GetString(5);

                        Verzorger verzorger = GetVerzorger(verzorgerID);
                        Dier dier = new Dier(dierID, naam, soort, leeftijd, null, verzorger);
                        dieren.Add(dier);
                    }
                }
            }
            return dieren;
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
