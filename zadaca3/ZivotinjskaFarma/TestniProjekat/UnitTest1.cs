using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZivotinjskaFarma;

namespace TestniProjekat
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ObuhvatOdlukaTest1()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new System.Collections.Generic.List<string> { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddDays(-365), 56, 1.56, lokacija);
            Proizvod proizvod = new Proizvod("Mlijeko", "Mlijeko", "Mlijeko", zivotinja, DateTime.Now, DateTime.Now.AddDays(7), 1);
            Assert.IsTrue(farma.KupovinaProizvoda(proizvod, DateTime.Now.AddDays(7), 1));
        }

        [TestMethod]
        public void ObuhvatOdlukaTest2()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new System.Collections.Generic.List<string> { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddDays(-365), 56, 1.56, lokacija);
            Proizvod proizvod = new Proizvod("Mlijeko", "Mlijeko", "Mlijeko", zivotinja, DateTime.Now, DateTime.Now.AddDays(7), 51);
      
            Assert.IsFalse(farma.KupovinaProizvoda(proizvod, DateTime.Now.AddDays(7), 51));
        }

        [TestMethod]
        public void ObuhvatOdlukaTest3()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new System.Collections.Generic.List<string> { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddDays(-365), 56, 1.56, lokacija);
            Proizvod proizvod = new Proizvod("Mlijeko", "Mlijeko", "Mlijeko", zivotinja, DateTime.Now, DateTime.Now.AddDays(7), 51);
            farma.KupovinaProizvoda(proizvod, DateTime.Now.AddDays(7), 1);
            Assert.IsTrue(farma.KupovinaProizvoda(proizvod, DateTime.Now.AddDays(7), 1));
        }


    }
}
