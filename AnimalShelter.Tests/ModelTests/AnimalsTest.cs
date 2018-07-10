using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalShelter.Models;
using System.Collections.Generic;

namespace AnimalShelter.Tests
{
    [TestClass]
    public class AnimalsTest : IDisposable
    {

        public void Dispose()
        {
            Animals.DeleteAll();
        }

        public AnimalsTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=shelter_test;";
        }

        [TestMethod]
        public void Save_SavesToDatabase_AnimalList()
        {
            //Arrange
            DateTime value = new DateTime(2017, 1, 18);
            Animals testAnimal = new Animals(0, "Bob Barker", "Dog", value, "Female");

            //Act
            testAnimal.Save();
            List<Animals> result = Animals.GetAll();
            List<Animals> testList = new List<Animals> { testAnimal };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }
    }
}
