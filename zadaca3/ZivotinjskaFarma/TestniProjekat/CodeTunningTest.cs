using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZivotinjskaFarma;

namespace TestniProjekat
{
    [TestClass]
    public class CodeTunningTest
    {
        [TestMethod]
        public void TestMethodForCodeTunning()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            for(int i = 0; i < 20500000; i++)
                        {
                            Zivotinja zivotinja1 = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddDays(-10), 5, 50, lokacija);
                            farma.Zivotinje.Add(zivotinja1);
                        }

            int x = 1;

            farma.RadSaZivotinjamaRefactoring("Dodavanje", zivotinja, 2000);
            
            int y = 1;


            


            Assert.IsTrue(farma.Zivotinje.Count == 20500001);
        }

       
    }
}
