﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZivotinjskaFarma;

namespace TestZadatak3
{
    [TestClass]
    public class ZivotinjaTest
    {

        static Lokacija lokacija;
        static Zivotinja zivotinja;

        [TestInitialize]
        public void Inizijaliziraj()
        {
            DateTime dt = new DateTime(2000, 1, 1);
            lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            zivotinja = new Zivotinja(ZivotinjskaVrsta.Krava, dt, 500, 2, lokacija);
        }

        [TestMethod]
        public void TestSetterStarost()
        {
            Assert.AreEqual(new DateTime(2000, 1, 1), zivotinja.Starost);
            zivotinja.Starost = new DateTime(2020, 1, 1);
            Assert.AreEqual(new DateTime(2020, 1, 1), zivotinja.Starost);
            Assert.ThrowsException<FormatException>(() => zivotinja.Starost = DateTime.Now.AddDays(10), "Životinja ne može biti rođena u budućnosti!");
        }

        [TestMethod]
        public void TestSetterTjelesnaMasa()
        {
            Assert.AreEqual(500, zivotinja.TjelesnaMasa);
            zivotinja.TjelesnaMasa = 100;
            Assert.AreEqual(100, zivotinja.TjelesnaMasa);
            Assert.ThrowsException<FormatException>(() => zivotinja.TjelesnaMasa = 0.01, "Tjelesna masa ne može biti manja od 0.1 kg!");
        }

        [TestMethod]
        public void TestSetterVisina()
        {
            Assert.AreEqual(2, zivotinja.Visina);
            zivotinja.Visina = 1;
            Assert.AreEqual(1, zivotinja.Visina);
            Assert.ThrowsException<FormatException>(() => zivotinja.Visina = 0.9, "Visina ne može biti manja od 1 cm!");
        }

        [TestMethod]
        public void ProvjeriStanjeZivotinjeTest1()  //Eldar Panjeta (18711)
        {
            DateTime dt = new DateTime(2000, 1, 1);
            Lokacija l = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja z = new Zivotinja(ZivotinjskaVrsta.Krava, dt, 500, 2, l);
            z.ProvjeriStanjeZivotinje();
            Assert.IsFalse(z.Proizvođač);
        }
        [TestMethod]
        public void ProvjeriStanjeZivotinjeTest2() //Eldar Panjeta (18711)
        {
            DateTime dt = new DateTime(2020, 1, 1);
            Lokacija l = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja z = new Zivotinja(ZivotinjskaVrsta.Krava, dt, 500, 2, l);
            z.ProvjeriStanjeZivotinje();
            Assert.IsTrue(z.Proizvođač);
        }
        [TestMethod]
        public void ProvjeriStanjeZivotinjeTest3() //Eldar Panjeta (18711)
        {
            DateTime dt = new DateTime(2013, 1, 1);
            Lokacija l = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja z = new Zivotinja(ZivotinjskaVrsta.Krava, dt, 500, 2, l);
            z.PregledajZivotinju("Krava", "Losa", "2,5");
            z.ProvjeriStanjeZivotinje();
            Assert.IsFalse(z.Proizvođač);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ProvjeriStanjeZivotinjeTest4() //Senija Kaleta (18662)
        {
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Magarac, DateTime.Parse("01/01/2012"), 70, 1.5, lokacija);
            zivotinja.PregledajZivotinju("", "", "");
            zivotinja.ProvjeriStanjeZivotinje();
            Assert.IsFalse(zivotinja.Proizvođač);
        }

        [TestMethod]
        public void ProvjeriStanjeZivotinjeTest5() //Senija Kaleta (18662)
        {
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Magarac, DateTime.Parse("01/01/2019"), 70, 1.5, lokacija);
            zivotinja.PregledajZivotinju("sve ok", "sve ok", "5");
            zivotinja.PregledajZivotinju("sve ok", "sve ok", "5");
            zivotinja.PregledajZivotinju("sve ok", "sve ok", "3");
            zivotinja.PregledajZivotinju("sve ok", "sve ok", "1");
            zivotinja.PregledajZivotinju("sve ok", "sve ok", "1");
            zivotinja.PregledajZivotinju("sve ok", "sve ok", "1");
            zivotinja.ProvjeriStanjeZivotinje();
            Assert.IsFalse(zivotinja.Proizvođač);
            zivotinja.Proizvođač = true;
            zivotinja.PregledajZivotinju("sve ok", "sve ok", "4");
            zivotinja.PregledajZivotinju("sve ok", "sve ok", "4,4");
            zivotinja.PregledajZivotinju("sve ok", "sve ok", "4,6");
            zivotinja.ProvjeriStanjeZivotinje();
            Assert.IsTrue(zivotinja.Proizvođač);
        }


    }
}
