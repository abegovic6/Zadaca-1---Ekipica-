using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml;
using ZivotinjskaFarma;

namespace TestZadatak3
{
    [TestClass]
    public class KupovinaTest
    {
        static Lokacija lokacija;
        static Zivotinja zivotinja;
        static int broj;
        static Proizvod proizvod;


        public static IEnumerable<object[]> UčitajIspravnePodatkeXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("podaciKupovineIspravni.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { elements[0], elements[1], elements[2], elements[3] };
            }
        }

        static IEnumerable<object[]> IspravneKupovineXML
        {
            get
            {
                return UčitajIspravnePodatkeXML();
            }
        }

        public static IEnumerable<object[]> UčitajNeispravnePodatkeXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("podaciKupovineNeispravni.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { elements[0], elements[1], elements[2], elements[3] };
            }
        }

        static IEnumerable<object[]> NeispravneKupovineXML
        {
            get
            {
                return UčitajNeispravnePodatkeXML();
            }
        }

        [TestInitialize]
        public void Inizijaliziraj()
        {
            lokacija = new Lokacija(new List<string>()
            { "Lokacija", "Zmaja od Bosne", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 1000);
            zivotinja = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            proizvod = new Proizvod("Vuna", "opis", "Vuna", zivotinja, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);
        }

        [TestMethod]
        [DynamicData("IspravneKupovineXML")]
        public void VerificirajKupovinuTestKupovineIspravne(string vrstazivotinje,
            string vrstaproizvoda, string datumkupovine, string rokisporuke)      // Begović Amila (18608)
        {
            Zivotinja z = new Zivotinja((ZivotinjskaVrsta)Enum.Parse(typeof(ZivotinjskaVrsta), vrstazivotinje),
                DateTime.Now.AddDays(-1), 5, 50, lokacija);
            Proizvod p = new("ime", "opis", vrstaproizvoda, z,
                DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);
            Kupovina k = new("2", DateTime.ParseExact(datumkupovine, "dd/MM/yyyy", null), DateTime.ParseExact(rokisporuke, "dd/MM/yyyy", null), p, 1, true);
            Assert.IsTrue(k.VerificirajKupovinu());
        }

        [TestMethod]
        [DynamicData("NeispravneKupovineXML")]
        public void VerificirajKupovinuTestKupovineNeispravne(string vrstazivotinje,
            string vrstaproizvoda, string datumkupovine, string rokisporuke)         // Begović Amila (18608)
        {
            Zivotinja z = new Zivotinja((ZivotinjskaVrsta)Enum.Parse(typeof(ZivotinjskaVrsta), vrstazivotinje),
                DateTime.Now.AddDays(-1), 5, 50, lokacija);
            Proizvod p = new("ime", "opis", vrstaproizvoda, z,
                DateTime.Now.AddDays(-1), DateTime.Now.AddDays(60), 100);
            Kupovina k = new("2", DateTime.ParseExact(datumkupovine, "dd/MM/yyyy", null), DateTime.ParseExact(rokisporuke, "dd/MM/yyyy", null), p, 1, true);
            Assert.IsFalse(k.VerificirajKupovinu());
        }

        [TestMethod]
        public void VerificirajKupovinuTest1() //Eldar Panjeta (18711)
        {
            Kupovina k = new Kupovina("2", DateTime.Now, DateTime.Now.AddDays(30), proizvod, 1, true);
            Assert.IsTrue(k.VerificirajKupovinu());
        }

        [TestMethod]
        public void VerificirajKupovinuTest2() //Eldar Panjeta (18711)
        {
            Kupovina k = new Kupovina("2", DateTime.Now, DateTime.Now.AddDays(30), proizvod, 101, true);
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



    }
}
