using System;
using System.Collections.Generic;

namespace ZivotinjskaFarma
{
    public class Zivotinja
    {
        #region Atributi

        int ID;
        ZivotinjskaVrsta vrsta;
        DateTime starost;
        double tjelesnaMasa, visina;
        List<string> pregledi;
        bool proizvođač;
        Lokacija prebivalište;
        static int brojac = 1;

        #endregion

        #region Properties

        public ZivotinjskaVrsta Vrsta { get => vrsta; set => vrsta = value; }
        public DateTime Starost
        {
            get => starost; 
            set
            {
                if (value > DateTime.Now)
                    throw new FormatException("Životinja ne može biti rođena u budućnosti!");
                starost = value;
            }
        }
        public double TjelesnaMasa
        {
            get => tjelesnaMasa;
            set
            {
                if (value < 0.1)
                    throw new FormatException("Tjelesna masa ne može biti manja od 0.1 kg!");
                tjelesnaMasa = value;
            }
        }
        public double Visina 
        { 
            get => visina; 
            set
            {
                if (value < 1)
                    throw new FormatException("Visina ne može biti manja od 1 cm!");
                visina = value;
            }
        }
        public List<string> Pregledi { get => pregledi; }
        public bool Proizvođač { get => proizvođač; set => proizvođač = value; }
        internal Lokacija Prebivalište { get => prebivalište; set => prebivalište = value; }
        public int ID1 { get => ID; }

        #endregion

        #region Konstruktor

        public Zivotinja(ZivotinjskaVrsta vrsta, DateTime starost, double masa, double visina, Lokacija prebivaliste)
        {
            ID = brojac;
            brojac++;
            Vrsta = vrsta;
            Starost = starost;
            TjelesnaMasa = masa;
            Visina = visina;
            pregledi = new List<string>();
            Proizvođač = true;
            Prebivalište = prebivaliste;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda koja vrši provjeru stanja životinje.
        /// Ukoliko je životinja starija od 10 godina, automatski prestaje biti proizvođač.
        /// Ukoliko je životinja starija od 7 godina i najnoviji pregled ima sadržaj "OCJENA : 3.5" ili manje,
        /// životinja prestaje biti proizvođač.
        /// U suprotnom, potrebno je izvršiti provjeru 3 najnovija pregleda i ukoliko je prosječna ocjena manja od 4,
        /// životinja prestaje biti proizvođač.
        /// </summary>
        public void ProvjeriStanjeZivotinje() // implementirala metodu: Begović Amila
        {
            double godinaStarosti = (double)((DateTime.Now - starost).TotalDays / 365.242199);
            if(godinaStarosti > 10)
            {
                proizvođač = false;
            } 
            else if(godinaStarosti > 7)
            {
                if(pregledi.Count > 0)
                {
                    string zadnjiPregled = pregledi[pregledi.Count - 1];
                    for (int i = 0; i < zadnjiPregled.Length; i++)
                    {
                        string substringZadnjegPregleda = zadnjiPregled.Substring(i);
                        if(substringZadnjegPregleda.StartsWith("OCJENA: "))
                        {
                        

                            if (Double.Parse(substringZadnjegPregleda.Substring(8)) < 3.5)
                            {
                                proizvođač = false;
                            }
                            break;
                        }
                    }
                }
            } else
            {
                if (pregledi.Count > 3)
                {
                    double suma = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        string zadnjiPregled = pregledi[pregledi.Count - 1 - i];
                        for (int j = 0; j < zadnjiPregled.Length; j++)
                        {

                            string substringZadnjegPregleda = zadnjiPregled.Substring(j);

                            if (substringZadnjegPregleda.StartsWith("OCJENA: "))
                            {
                                suma += Double.Parse(substringZadnjegPregleda.Substring(8));
                                break;
                            }
                        }
                    }

                    if (!(suma / 3 > 4))
                    {
                        proizvođač = false;
                    }
                    
                }

            }

        }

        public void PregledajZivotinju(string osnovneInfo, string napomena, string ocjena)
        {
            string pregled = "OSNOVNE INFORMACIJE: " + osnovneInfo + "\n"
                            + "NAPOMENA: " + napomena + "\n"
                            + "OCJENA: " + ocjena;

            pregledi.Add(pregled);
        }

        #endregion
    }
}
