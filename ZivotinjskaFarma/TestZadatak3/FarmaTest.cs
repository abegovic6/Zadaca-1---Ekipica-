using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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

        public static IEnumerable<object[]> UcitajPorez1()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("PodaciZaFarmuPorez1.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { elements[0], elements[1]};
            }
        }

        static IEnumerable<object[]> Porez1XML
        {
            get
            {
                return UcitajPorez1();
            }
        }


        [TestMethod]
        [DynamicData("Porez1XML")]
        public void ObracunajPorez1postoTest(string grad, string povrsina) // Amila Begović (18608)
        {
            Farma farma = new Farma();
            Lokacija lokacija1 = new Lokacija(new List<string> {"Lokacija1", "Ulica", "5", grad, "71000",
                "Bosna i Hercegovina" }, Double.Parse(povrsina));

            farma.DodavanjeNoveLokacije(lokacija1);
            Assert.IsTrue(Math.Abs(10 * 0.01 - farma.ObračunajPorez()) < 0.00001);
        }

        public static IEnumerable<object[]> UcitajPorez3()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("PodaciZaFarmuPorez3.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { elements[0], elements[1] };
            }
        }

        static IEnumerable<object[]> Porez3XML
        {
            get
            {
                return UcitajPorez3();
            }
        }

        [TestMethod]
        [DynamicData("Porez3XML")]
        public void ObracunajPorez3postoTest(string grad, string povrsina) // Amila Begović (18608)
        {
            Farma farma = new Farma();
            Lokacija lokacija1 = new Lokacija(new List<string> {"Lokacija1", "Ulica", "5", grad, "71000",
                "Bosna i Hercegovina" }, Double.Parse(povrsina));

            farma.DodavanjeNoveLokacije(lokacija1);
            Assert.IsTrue(Math.Abs(10 * 0.03 - farma.ObračunajPorez()) < 0.00001);
        }



        [TestMethod]
        public void RadSaZivotinjamaDodavanjeTest()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            farma.RadSaZivotinjama("Dodavanje", zivotinja);
            Assert.IsTrue(farma.Zivotinje.Contains(zivotinja));

            Assert.ThrowsException<ArgumentException>(() => farma.RadSaZivotinjama("Dodavanje", zivotinja),
                "Životinja je već registrovana u bazi!");
        }

        [TestMethod]
        public void RadSaZivotinjamaIzmjenaTest()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            Assert.ThrowsException<ArgumentException>(() => farma.RadSaZivotinjama("Izmjena", zivotinja),
                "Životinja nije registrovana u bazi!");
            farma.RadSaZivotinjama("Dodavanje", zivotinja);
            zivotinja.TjelesnaMasa = 20;
            farma.RadSaZivotinjama("Izmjena", zivotinja);
            Zivotinja izmjenjena = farma.Zivotinje.Find(z => z.ID1 == zivotinja.ID1);
            Assert.AreEqual(20, izmjenjena.TjelesnaMasa);
        }

        [TestMethod]
        public void RadSaZivotinjamaBrisanjeTest()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            Assert.ThrowsException<ArgumentException>(() => farma.RadSaZivotinjama("Brisanje", zivotinja),
                "Životinja nije registrovana u bazi!");
            farma.RadSaZivotinjama("Dodavanje", zivotinja);
            Assert.IsTrue(farma.Zivotinje.Contains(zivotinja));
            farma.RadSaZivotinjama("Brisanje", zivotinja);
            Assert.IsFalse(farma.Zivotinje.Contains(zivotinja));
        }

        [TestMethod]
        public void DodavanjeNoveLokacijeTest()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            farma.DodavanjeNoveLokacije(lokacija);
            Assert.IsTrue(farma.Lokacije.Contains(lokacija));

            Lokacija lokacija2 = new Lokacija(new List<string>()
            { "Lokacija1", "Zmaja od Bosne", "2", "Sarajevo", "71300", "Bosna i Hercegovina" }, 2000);

            Assert.ThrowsException<InvalidOperationException>(() => farma.DodavanjeNoveLokacije(lokacija2),
                "Ista lokacija je već zabilježena!");

            lokacija2.Grad = "Bihać";
            farma.DodavanjeNoveLokacije(lokacija2);
            Assert.IsTrue(farma.Lokacije.Contains(lokacija2));
        }


        static List<int> praznici;

        static IEnumerable<object[]> PrazniciCSVNeispravni
        {
            get
            {
                return UčitajPodatkeCSV("podaciPraznikNeispravni.csv");
            }
        }

        static IEnumerable<object[]> PrazniciCSVIspravni
        {
            get
            {
                return UčitajPodatkeCSV("podaciPraznikIspravni.csv");
            }
        }

        public static IEnumerable<object[]> UčitajPodatkeCSV(string file)
        {
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();



                    yield return new object[]
                    {
                        DateTime.ParseExact(elements[0], "dd/MM/yyyy", null)
                    };
                }
            }
        }



        [TestMethod]
        [DynamicData("PrazniciCSVIspravni")]
        public void PraznikPodaciIspravniTest(DateTime datum)
        {
            Assert.IsTrue(Farma.Praznik(datum));

        }

        [TestMethod]
        [DynamicData("PrazniciCSVNeispravni")]
        public void PraznikPodaciNeispravniTest(DateTime datum)
        {
            Assert.IsFalse(Farma.Praznik(datum));
        }

        [TestMethod]
        public void KupovinaProizvodaTest()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            Proizvod proizvod = new Proizvod("Vuna", "opis", "Vuna", 
                zivotinja, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);

            Assert.IsFalse(farma.KupovinaProizvoda(proizvod, DateTime.Now.AddDays(30), 101));
            Assert.IsTrue(farma.KupovinaProizvoda(proizvod, DateTime.Now.AddDays(30), 1));
            Assert.AreEqual(1, farma.Kupovine.Count(k => k.KupljeniProizvod == proizvod));
        }

        [TestMethod] 
        public void BrisanjeKupovineTest()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            Proizvod proizvod = new Proizvod("Vuna", "opis", "Vuna",
                zivotinja, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);
            farma.KupovinaProizvoda(proizvod, DateTime.Now.AddDays(30), 1);
            
            Kupovina kupovina = new Kupovina("10", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(4), proizvod, 1, false);
            farma.BrisanjeKupovine(kupovina); // ne bi se smio baciti izuzetak niti izbaciti ništa iz lista
            Assert.AreEqual(1, farma.Kupovine.Count);

            farma.BrisanjeKupovine(farma.Kupovine[0]); // izbacuje element iz liste
            Assert.AreEqual(0, farma.Kupovine.Count);
        }

        [TestMethod]
        public void ObaviSistemskiPregledTest()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja zivotinja1 = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            Zivotinja zivotinja2 = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);

            farma.RadSaZivotinjama("Dodavanje", zivotinja1);
            farma.RadSaZivotinjama("Dodavanje", zivotinja2);

            List<List<String>> informacije = new List<List<string>>() { new List<string>(){ "osnovne info", "naomena", "2"},
                new List<string>() { "osnovne info", "naomena", "2" }};

            Assert.AreEqual(0, zivotinja1.Pregledi.Count);
            Assert.AreEqual(0, zivotinja2.Pregledi.Count);

            farma.ObaviSistematskiPregled(informacije);

            Assert.AreEqual(1, zivotinja1.Pregledi.Count);
            Assert.AreEqual(1, zivotinja2.Pregledi.Count);

        }

        [TestMethod]
        public void ProizvodiTest()
        {
            Farma farma = new Farma();
            Lokacija lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            Proizvod proizvod1 = new Proizvod("Vuna", "opis", "Vuna",
                zivotinja, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);
            Proizvod proizvod2 = new Proizvod("Vuna", "opis", "Vuna",
                zivotinja, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);
            Proizvod proizvod3 = new Proizvod("Vuna", "opis", "Vuna",
                zivotinja, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);

            farma.Proizvodi.Add(proizvod1);
            farma.Proizvodi.Add(proizvod2);
            farma.Proizvodi.Add(proizvod3);
            Assert.AreEqual(3, farma.Proizvodi.Count);
        }


    }
}
