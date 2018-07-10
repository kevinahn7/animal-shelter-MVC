using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AnimalShelter.Models
{
    public class Animals
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public DateTime Date { get; set; }
        public string Gender { get; set; }

        public Animals(int Id, string animalName, string species, DateTime date, string gender)
        {
            this.Id = Id;
            this.Name = animalName;
            this.Species = species;
            this.Date = date;
            this.Gender = gender;
        }

        public Animals(string animalName, string species, DateTime date, string gender)
        {
            this.Name = animalName;
            this.Species = species;
            this.Date = date;
            this.Gender = gender;
        }

        public string ConvertDate(DateTime date)
        {
            string format = "MMM d yyyy";
            string newDate = date.ToString(format);
            return newDate;
        }

        public override bool Equals(System.Object otherAnimal)
        {
            if (!(otherAnimal is Animals))
            {
                return false;
            }
            else
            {
                Animals newAnimal = (Animals) otherAnimal;
                bool nameEquality = (this.Name == newAnimal.Name);
                bool speciesEquality = (this.Species == newAnimal.Species);
                bool dateEquality = (this.Date == newAnimal.Date);
                bool genderEquality = (this.Gender == newAnimal.Gender);
                return (nameEquality && speciesEquality && dateEquality && genderEquality);
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM animals;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteSingle(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM animals WHERE id = " + id + ";";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Animals> GetAll()
        {
            List<Animals> allAnimals = new List<Animals> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM animals;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int animalId = rdr.GetInt32(0);
                string animalName = rdr.GetString(1);
                string animalSpecies = rdr.GetString(2);
                DateTime animalDate = rdr.GetDateTime(3);
                string animalGender = rdr.GetString(4);
                Animals newAnimal = new Animals(animalId, animalName, animalSpecies, animalDate, animalGender);
                allAnimals.Add(newAnimal);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allAnimals;
        }


        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO animals (name, species, date, gender) VALUES (@AnimalName, @AnimalSpecies, @AnimalDate, @AnimalGender);";

            cmd.Parameters.AddWithValue("@AnimalName", this.Name);
            cmd.Parameters.AddWithValue("@AnimalSpecies", this.Species);
            cmd.Parameters.AddWithValue("@AnimalDate", this.Date);
            cmd.Parameters.AddWithValue("@AnimalGender", this.Gender);

            cmd.ExecuteNonQuery();
            this.Id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


    }
}
