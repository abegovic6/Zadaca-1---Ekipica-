using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZivotinjskaFarma;

namespace TestZadatak3
{
    [TestClass]
    public class KupovinaTest
    {
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
        public void VerificirajKupovinuTest1() //Eldar Panjeta (18711)
        {
            Lokacija l = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja z = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, l);
            Proizvod p = new Proizvod("Vuna", "opis", "Vuna", z, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);
            Kupovina k = new Kupovina("2", DateTime.Now, DateTime.Now.AddDays(31), p, 1, true);
            Assert.IsTrue(k.VerificirajKupovinu());
        }
        [TestMethod]
        public void VerificirajKupovinuTest2() //Eldar Panjeta (18711)
        {
            Lokacija l = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja z = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, l);
            Proizvod p = new Proizvod("Vuna", "opis", "Vuna", z, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);
            Kupovina k = new Kupovina("2", DateTime.Now, DateTime.Now.AddDays(31), p, 101, true);
            Assert.IsFalse(k.VerificirajKupovinu());
        }
    }
}
