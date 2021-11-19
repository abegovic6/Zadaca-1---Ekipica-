using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZivotinjskaFarma;

namespace TestZadatak3
{
    [TestClass]
    public class ProizvodTest
    {
        static Lokacija prebivaliste;
        static Zivotinja proizvodjac;
        static Proizvod proizvod;

        [TestInitialize]
        public void Inizijaliziraj()
        {
            prebivaliste = new Lokacija(new List<string>()
            { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            proizvodjac = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddDays(-365), 10, 1.5, prebivaliste);
            proizvod = new Proizvod("Mlijeko", "Svjeze mlijeko", "Mlijeko", proizvodjac, DateTime.Now, DateTime.Now.AddDays(7), 1);

        }
        [TestMethod]
        public void TestSetterVrsta1()
        {
            //test osnovnih postavki
            Assert.AreEqual(proizvod.Proizvođač.Vrsta, ZivotinjskaVrsta.Koza);
            Assert.AreEqual(proizvod.Vrsta, "Mlijeko");
            Assert.IsTrue(proizvod.DatumProizvodnje.Date == DateTime.Parse("11/19/2021")); // ovo provjeriti neki drugi dan vidjeti hoce li raditi
            Assert.ThrowsException<InvalidOperationException>(() => proizvod.Vrsta="Meso", "Unijeli ste vrstu proizvoda koja ne postoji!");
        }

        [TestMethod]
       // [ExpectedException(typeof(InvalidOperationException))]
        public void TestSetterProizvodjac()
        {
            Zivotinja pomocna = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddDays(-365), 10, 1.5, prebivaliste);
            proizvod.Proizvođač = pomocna;
            Assert.IsTrue(proizvod.Proizvođač.Vrsta == ZivotinjskaVrsta.Krava);
            pomocna.Vrsta = ZivotinjskaVrsta.Ovca;
            Assert.IsFalse(proizvod.Proizvođač.Vrsta == ZivotinjskaVrsta.Krava);
            pomocna.Vrsta = ZivotinjskaVrsta.Magarac;
            Assert.ThrowsException<InvalidOperationException>(() => { pomocna.Vrsta = ZivotinjskaVrsta.Patka;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");


        }

    }
}
