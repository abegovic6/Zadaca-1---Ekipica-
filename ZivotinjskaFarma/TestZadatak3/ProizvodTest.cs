using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZivotinjskaFarma;

namespace TestZadatak3
{
    [TestClass]
    public class ProizvodTest
    {
        [TestMethod]
        public void TestVrstaSetter1()
        {
            //test osnovnih postavki
            Lokacija prebivaliste = new Lokacija(new List<string>()
            { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            Zivotinja proizvodjac = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddDays(-365), 10, 1.5, prebivaliste);
            Proizvod p = new Proizvod("Mlijeko", "Svjeze mlijeko", "Mlijeko", proizvodjac, DateTime.Now, DateTime.Now.AddDays(7), 1);
            Assert.AreEqual(p.Proizvođač.Vrsta, ZivotinjskaVrsta.Koza);
            Assert.AreEqual(p.Vrsta, "Mlijeko");
            Assert.IsTrue(p.DatumProizvodnje.Date == DateTime.Parse("11/19/2021")); // ovo provjeriti neki drugi dan vidjeti hoce li raditi

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestVrstaSetter2()
        {
            Lokacija prebivaliste = new Lokacija(new List<string>()
            { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            Zivotinja proizvodjac = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddDays(-365), 10, 1.5, prebivaliste);
            Proizvod p = new Proizvod("Mlijeko", "Svjeze mlijeko", "Piće", proizvodjac, DateTime.Now, DateTime.Now.AddDays(7), 1);
        }

        [TestMethod]
        public void TestVrstaSetter3()
        {
            Lokacija prebivaliste = new Lokacija(new List<string>()
            { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            Zivotinja proizvodjac = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddDays(-365), 10, 1.5, prebivaliste);
            Assert.ThrowsException<InvalidOperationException>(() => new Proizvod("Mlijeko", "Svjeze mlijeko", "Meso", proizvodjac, DateTime.Now, DateTime.Now.AddDays(7), 1), "Unijeli ste vrstu proizvoda koja ne postoji!");
        }




    }
}
