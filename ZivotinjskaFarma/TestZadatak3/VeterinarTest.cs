using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZivotinjskaFarma;

namespace TestZadatak3
{
    [TestClass]
    public class VeterinarTest
    {
        static Zivotinja ovca;
        static Zivotinja patka;
        static Lokacija lokacija;
        static Veterinar veterinar;
        static Mock mock;

        [TestInitialize]
        public void Inizijaliziraj()
        {
            lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            ovca = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            patka = new Zivotinja(ZivotinjskaVrsta.Patka, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            veterinar = new Veterinar();
            mock = new Mock();

        }
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestOcjenaZdravstvenogStanja1()
        {
            veterinar.ocjenaZdravstvenogStanjaZivotinje(ovca);

        }
        [TestMethod]
        public void TestOcjenaZdravstvenogStanja2()
        {
            Assert.AreEqual(4, mock.ocjenaZdravstvenogStanjaZivotinje(ovca));
            Assert.AreEqual(6, mock.ocjenaZdravstvenogStanjaZivotinje(patka));
        }

    }
}
