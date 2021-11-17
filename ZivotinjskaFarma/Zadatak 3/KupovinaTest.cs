using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZivotinjskaFarma;

namespace Zadatak_3
{
    [TestClass]
    class KupovinaTest
    {
        [TestMethod]
        public void VerificirajKupovinu()
        {
            Lokacija l = new Lokacija(new List<string>()
            { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Patka, DateTime.Now.AddDays(-1), 5, 50, l);
            Proizvod p = new Proizvod("ime", "opis", "vrsta", z1, DateTime.Now.AddDays(-1), DateTime.Now, 10);
            Kupovina k = new Kupovina("1", DateTime.Now, DateTime.Now, p, 5, true);

            Assert.IsFalse(k.VerificirajKupovinu());

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
    }
}
