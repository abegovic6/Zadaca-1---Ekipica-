using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZivotinjskaFarma;

namespace TestZadatak3
{
    [TestClass]
    public class ZivotinjaTest
    {
        [TestMethod]
        public void ProvjeriStanjeZivotinjeTest1() //Eldar Panjeta (18711)
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
            z.PregledajZivotinju("Krava", "Losa", "3.1");
            z.ProvjeriStanjeZivotinje();
            Assert.IsFalse(z.Proizvođač);
        }
    }
}
