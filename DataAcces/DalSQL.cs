using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCode.Models;

namespace TemplateCode.DataAcces
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
            serverName = "LAPTOP-5FM0T3FM";
            databaseName = "DBTechTerraZoo";
            connectionString = $"Server={serverName};Database={databaseName};Trusted_Connection=True;TrustServerCertificate=True;";
        }

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

                        Dier dier = new Dier(dierID, naam, soort, leeftijd, null, null);
                        dieren.Add(dier);
                    }
                }
            }

            return dieren;
        }
    }

}
