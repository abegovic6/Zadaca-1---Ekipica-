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
            //Assert.IsTrue(proizvod.DatumProizvodnje.Date == DateTime.Parse("11/19/2021")); // ovo provjeriti neki drugi dan vidjeti hoce li raditi
            Assert.ThrowsException<InvalidOperationException>(() => proizvod.Vrsta="Meso", "Unijeli ste vrstu proizvoda koja ne postoji!");
        }

        [TestMethod]
        public void TestSetterProizvodjac()
        {
            Zivotinja pomocna = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddDays(-365), 10, 1.5, prebivaliste);
            proizvod.Proizvođač = pomocna;
            Assert.IsTrue(proizvod.Proizvođač.Vrsta == ZivotinjskaVrsta.Krava);
            pomocna.Vrsta = ZivotinjskaVrsta.Ovca;
            Assert.IsFalse(proizvod.Proizvođač.Vrsta == ZivotinjskaVrsta.Krava);
            //pomocna.Vrsta = ZivotinjskaVrsta.Magarac;
            /*Assert.ThrowsException<InvalidOperationException>(() => { pomocna.Vrsta = ZivotinjskaVrsta.Patka;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");*/

            //Jaja
            proizvod.Vrsta = "Jaja";
            Assert.ThrowsException<InvalidOperationException>(() => { pomocna.Vrsta = ZivotinjskaVrsta.Krava;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");

            Assert.ThrowsException<InvalidOperationException>(() => {
                pomocna.Vrsta = ZivotinjskaVrsta.Magarac;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");

            Assert.ThrowsException<InvalidOperationException>(() => {
                pomocna.Vrsta = ZivotinjskaVrsta.Ovca;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");

            Assert.ThrowsException<InvalidOperationException>(() => {
                pomocna.Vrsta = ZivotinjskaVrsta.Koza;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");


            //Proizvodjaci Vune
            proizvod.Vrsta = "Vuna";
            Assert.ThrowsException<InvalidOperationException>(() => {
                pomocna.Vrsta = ZivotinjskaVrsta.Patka;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");

            Assert.ThrowsException<InvalidOperationException>(() => {
                pomocna.Vrsta = ZivotinjskaVrsta.Kokoška;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");

            Assert.ThrowsException<InvalidOperationException>(() => {
                pomocna.Vrsta = ZivotinjskaVrsta.Guska;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");

            Assert.ThrowsException<InvalidOperationException>(() => {
                pomocna.Vrsta = ZivotinjskaVrsta.Krava;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");

            Assert.ThrowsException<InvalidOperationException>(() => {
                pomocna.Vrsta = ZivotinjskaVrsta.Magarac;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");

            Assert.ThrowsException<InvalidOperationException>(() => {
                pomocna.Vrsta = ZivotinjskaVrsta.Koza;
                proizvod.Proizvođač = pomocna;
            }, "Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");


        }
        [TestMethod]
        public void TestSetterProizvodjac2()
        {
            //MLijeko po defaultu je ovdje
            proizvod.Proizvođač.Vrsta = ZivotinjskaVrsta.Koza;
            Assert.IsFalse(proizvod.Proizvođač.Vrsta == ZivotinjskaVrsta.Krava);

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                proizvodjac.Vrsta = ZivotinjskaVrsta.Guska;
                proizvod.Proizvođač = proizvodjac;
            });

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                proizvodjac.Vrsta = ZivotinjskaVrsta.Patka;
                proizvod.Proizvođač = proizvodjac;
            });

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                proizvodjac.Vrsta = ZivotinjskaVrsta.Kokoška;
                proizvod.Proizvođač = proizvodjac;
            });

            //Sir
            proizvod.Vrsta = "Sir";
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                proizvodjac.Vrsta = ZivotinjskaVrsta.Magarac;
                proizvod.Proizvođač = proizvodjac;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDatumProizvodnje()
        {
            proizvod.DatumProizvodnje = proizvod.DatumProizvodnje.AddDays(100);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRokTRajanja()
        {
            //treba za getter test :))

            proizvod.RokTrajanja = proizvod.DatumProizvodnje.AddDays(-100);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestKolicinaNaStanju()
        {
            proizvod.KoličinaNaStanju = 0;
        }






    }
}
