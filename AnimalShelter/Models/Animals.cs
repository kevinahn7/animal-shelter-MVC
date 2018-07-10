using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AnimalShelter.Models
{
    public class Animals
    {
        private int _id;
        private string _name;
        private string _species;
        private DateTime _date;
        private string _gender;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public DateTime Date { get; set; }
        public string Gender { get; set; }

        public Animals(int Id, string animalName, string species, DateTime date, string gender)
        {
            _id = Id;
            _name = animalName;
            _species = species;
            _date = date;
            _gender = gender;
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
                bool NameEquality = (this.Name == newAnimal.Name);
                bool SpeciesEquality = (this.Species == newAnimal.Species);
                bool DateEquality = (this.Date == newAnimal.Date);
                bool GenderEquality = (this.Gender == newAnimal.Gender);
                return (NameEquality && SpeciesEquality && DateEquality && GenderEquality);
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
            cmd.CommandText = @"INSERT INTO animals (id, name, species, date, gender) VALUES (@AnimalId, @AnimalName, @AnimalSpecies, @AnimalDate, @AnimalGender);";

            MySqlParameter animalId = new MySqlParameter();
            animalId.ParameterName = "@AnimalId";
            animalId.Value = _id;
            cmd.Parameters.Add(animalId);

            MySqlParameter animalName = new MySqlParameter();
            animalName.ParameterName = "@AnimalName";
            animalName.Value = _name;
            cmd.Parameters.Add(animalName);

            MySqlParameter animalSpecies = new MySqlParameter();
            animalSpecies.ParameterName = "@AnimalSpecies";
            animalSpecies.Value = _species;
            cmd.Parameters.Add(animalSpecies);

            MySqlParameter animalDate = new MySqlParameter();
            animalDate.ParameterName = "@AnimalDate";
            animalDate.Value = _date;
            cmd.Parameters.Add(animalDate);

            MySqlParameter animalGender = new MySqlParameter();
            animalGender.ParameterName = "@AnimalGender";
            animalGender.Value = _gender;
            cmd.Parameters.Add(animalGender);


            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


    }
}
