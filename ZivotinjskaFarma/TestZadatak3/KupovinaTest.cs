using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZivotinjskaFarma;

namespace TestZadatak3
{
    [TestClass]
    public class KupovinaTest
    {

        static Lokacija lokacija;
        static Zivotinja zivotinja;
        static Proizvod proizvod;
        static int broj;

        [TestInitialize]
        public void Inizijaliziraj()
        {
            lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            zivotinja = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            proizvod = new Proizvod("Vuna", "opis", "Vuna", zivotinja, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);
        }

        [TestMethod]
        public void Vertest()
        {
            Lokacija l = new Lokacija(new List<string>()
            { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Patka, DateTime.Now.AddDays(-1), 5, 50, l);
            Proizvod p = new Proizvod("ime", "opis", "vrsta", z1, DateTime.Now.AddDays(-1), DateTime.Now, 10);
            Kupovina k = new Kupovina("1", DateTime.Now, DateTime.Now, p, 5, true);

            Assert.IsFalse(k.VerificirajKupovinu());

        }
        [TestMethod]
        public void VerificirajKupovinuTest1() 
        {
            Kupovina k = new Kupovina("2", DateTime.Now, DateTime.Now.AddDays(31), proizvod, 1, true);
            Assert.IsTrue(k.VerificirajKupovinu());
        }
        [TestMethod]
        public void VerificirajKupovinuTest2() 
        {
            Kupovina k = new Kupovina("2", DateTime.Now, DateTime.Now.AddDays(31), proizvod, 101, true);
            Assert.IsFalse(k.VerificirajKupovinu());
        }
        [TestMethod]
        public void TestIdKupca()
        {
            Kupovina k = new Kupovina("2", DateTime.Now, DateTime.Now.AddDays(31), proizvod, 1, true);
            Assert.AreEqual("2", k.IDKupca1);
            k.IDKupca1 = "1";
            Assert.AreEqual("1", k.IDKupca1);
        }

        [TestMethod]
        public void TestBrojac()
        {
            Kupovina k = new Kupovina("2", DateTime.Now, DateTime.Now.AddDays(31), proizvod, 1, true);
            Assert.AreEqual(1, Kupovina.DajSljedeciBroj());
        }
    }
}
