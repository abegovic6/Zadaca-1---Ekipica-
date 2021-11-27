﻿using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ZivotinjskaFarma;

namespace TestZadatak3
{
    [TestClass]
    public class LokacijaTest
    {
        static Lokacija lokacija;

        static IEnumerable<object[]> LokacijeCSVNeispravne
        {
            get
            {
                return UčitajPodatkeCSV("podaciLokacijaNeispravni.csv");
            }
        }

        static IEnumerable<object[]> LokacijeCSVIspravne
        {
            get
            {
                return UčitajPodatkeCSV("podaciLokacijaIspravni.csv");
            }
        }

        public static IEnumerable<object[]> UčitajPodatkeCSV(string file)
        {
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();

                    

                    yield return new object[]
                    {
                        new List<string>(){ elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]}, Double.Parse(elements[6], CultureInfo.InvariantCulture) 
                    };
                }
            }
        }

        [TestInitialize]
        public void Inizijaliziraj()
        {
            lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
        }

        [TestMethod]
        [DynamicData("LokacijeCSVNeispravne")]
        public void TestKonstruktorCSVNeispravni(List<string> lista, double povrsina)
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                Lokacija lokacija = new Lokacija(lista, povrsina);
            });
        }
        [TestMethod]
        [DynamicData("LokacijeCSVIspravne")]
        public void TestKonstruktorCSVIspravni(List<string> lista, double povrsina)
        {

            Lokacija lokacija = new Lokacija(lista, povrsina);

        }

        //Kroz sve testove za settere se također testiraju i getteri
        [TestMethod]
        public void TestSetterNaziv()
        {
            Assert.AreEqual("Lokacija", lokacija.Naziv);
            lokacija.Naziv = "Nova Lokacija";
            Assert.AreEqual("Nova Lokacija", lokacija.Naziv);
            Assert.ThrowsException<ArgumentException>(() => lokacija.Naziv = "", "Naziv ne smije biti prazan!");
        }
        [TestMethod]
        public void TestSetterAdresa()
        {
            Assert.AreEqual("Ulica", lokacija.Adresa);
            lokacija.Adresa = "Nova Adresa";
            Assert.AreEqual("Nova Adresa", lokacija.Adresa);
            Assert.ThrowsException<ArgumentException>(() => lokacija.Adresa = " ", "Adresa ne smije biti prazna!");
        }
        [TestMethod]
        public void TestSetterGrad()
        {
            Assert.AreEqual("Sarajevo", lokacija.Grad);
            lokacija.Grad = "Tuzla";
            Assert.AreEqual("Tuzla", lokacija.Grad);
            Assert.ThrowsException<ArgumentException>(() => lokacija.Grad = "Ankara", "Unijeli ste grad koji trenutno nije podržan!");
        }
        [TestMethod]
        public void TestSetterDržava()
        {
            Assert.AreEqual("Bosna i Hercegovina", lokacija.Država);
            lokacija.Država = "Hrvatska";
            Assert.AreEqual("Hrvatska", lokacija.Država);
            Assert.ThrowsException<ArgumentException>(() => lokacija.Država = "Njemačka", "Unijeli ste državu koja trenutno nije podržana!");
        }
        [TestMethod]
        public void TestSetterBrojUlice()
        {
            Assert.AreEqual(2, lokacija.BrojUlice);
            lokacija.BrojUlice = 22;
            Assert.AreEqual(22, lokacija.BrojUlice);
            Assert.ThrowsException<ArgumentException>(() => lokacija.BrojUlice = -10, "Broj ulice ne može biti manji od 1!");
        }
        [TestMethod]
        public void TestSetterPoštanskiBroj()
        {
            Assert.AreEqual(71000, lokacija.PoštanskiBroj);
            lokacija.PoštanskiBroj = 72000;
            Assert.AreEqual(72000, lokacija.PoštanskiBroj);
            Assert.ThrowsException<ArgumentException>(() => lokacija.PoštanskiBroj = 100000, "Unijeli ste pogrešan poštanski broj!");
            Assert.ThrowsException<ArgumentException>(() => lokacija.PoštanskiBroj = 9999, "Unijeli ste pogrešan poštanski broj!");
        }
        [TestMethod]
        public void TestSetterPovršina()
        {
            Assert.AreEqual(100, lokacija.Površina);
            lokacija.Površina = 2000;
            Assert.AreEqual(2000, lokacija.Površina);
            Assert.ThrowsException<ArgumentException>(() => lokacija.Površina = 0.0001, "Površina zemljišta mora biti barem 0.01 m2!");
        }
       
        [TestMethod]
        public void TestKonstruktorNeispravanBrojParametara()
        {
            Lokacija lokacija2;
            Assert.ThrowsException<ArgumentException>(() => {
                lokacija2 = new Lokacija(new List<string>()
            { "Lokacija", "Ulica", "2", "Sarajevo"}, 100);
            }, "Neispravan broj parametara!");
        }
    }
}
