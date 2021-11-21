using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml;
using ZivotinjskaFarma;
namespace TestZadatak3
{
    [TestClass]
    public class FarmaTest
    {

        [TestMethod]
        public void ObracunajPorezTest() //Senija Kaleta (18662)
        {
            Farma farma = new Farma();
            //osnovni slucaj
            Assert.AreEqual(0, farma.ObračunajPorez());
            double povrsina = 10001;
            double osnovica = 10;
            double epsilon = 0.0001;
            Lokacija lokacija1 = new Lokacija(new List<string> {"Lokacija1", "Ferhadija", "5", "Rijeka", "71000",
                "Bosna i Hercegovina" }, povrsina);
            farma.DodavanjeNoveLokacije(lokacija1);
            Assert.AreEqual(osnovica * 0.02, farma.ObračunajPorez());
            farma.BrisanjeLokacije(lokacija1);

            povrsina = 10000;
            Lokacija lokacija2 = new Lokacija(new List<string> {"Lokacija1", "Ferhadija", "5", "Sarajevo", "71000",
                "Bosna i Hercegovina" }, povrsina);
            farma.DodavanjeNoveLokacije(lokacija2);
            Assert.AreEqual(osnovica * 0.015, farma.ObračunajPorez());
            farma.BrisanjeLokacije(lokacija2);

            Lokacija lokacija3 = new Lokacija(new List<string> {"Lokacija1", "Zagrebacka", "5", "Zagreb", "71006",
                "Hrvatska" }, povrsina);
            farma.DodavanjeNoveLokacije(lokacija3);
            Assert.AreEqual(osnovica * 0.05, farma.ObračunajPorez());

            //sumarni ukupni test
            farma.DodavanjeNoveLokacije(lokacija1);
            farma.DodavanjeNoveLokacije(lokacija2);
            Assert.IsTrue(farma.ObračunajPorez() - (osnovica * 0.05 + osnovica * 0.015 + osnovica * 0.02) <= epsilon);
        }
    }
}
