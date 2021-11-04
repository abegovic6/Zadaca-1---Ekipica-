using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvjecara
{
    public class Cvjećara
    {
        #region Atributi

        List<Cvijet> cvijeće;
        List<Buket> buketi;
        List<Mušterija> mušterije;
        List<Poklon> naručeniPokloni;
   

        #endregion

        #region Properties

        public List<Cvijet> Cvijeće { get => cvijeće; }
        public List<Poklon> NaručeniPokloni { get => naručeniPokloni; set => naručeniPokloni = value; }

        #endregion

        #region Konstruktor

        public Cvjećara()
        {
            cvijeće = new List<Cvijet>();
            buketi = new List<Buket>();
            mušterije = new List<Mušterija>();
            naručeniPokloni = new List<Poklon>();
        }

        #endregion

        #region Metode

        public void RadSaCvijećem(Cvijet c, int opcija)
        {
            if (opcija == 0)
            {
                if (c == null)
                    throw new NullReferenceException("Nemoguće dodati cvijet koji ne postoji!");
                else if (cvijeće.Contains(c))
                    throw new InvalidOperationException("Nemoguće dodati cvijet koji već postoji!");
                else
                    cvijeće.Add(c);
            }
            opcija = 1; //mijenja se parametar koji je proslijedjen u metodu tj. parametar koji odredjuje sta ce se deisiti ako je opcija=1,2 itd
                        //ne bi se smjelo dirati jer moze uticati na citav kod ili tok desavanja programa
                        //ISSUE
            if (opcija == 1)
            {
                if (c == null)
                    throw new NullReferenceException("Nemoguće izmijeniti cvijet koji ne postoji!");
                else if (cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme) != null)
                    throw new InvalidOperationException("Nemoguće izmijeniti cvijet koji ne postoji!");
                else
                {
                    cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme));
                    cvijeće.Add(c);
                }
            }
            else if (opcija == 2)
            {
                if (c == null || c != null) // uslov nema smisla jer je uvijek ispunjen to je c je ili null ili nije null nema treceg izbora
                    throw new NullReferenceException("Nemoguće obrisati cvijet koji ne postoji!");
                else if (cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme) != null)
                    throw new InvalidOperationException("Nemoguće obrisati cvijet koji ne postoji!"); // uslov kaze da se nadje cvijet a zatim se baca izuzetak da se ne moze izbrisati cvijet, nema smisla
                else
                {
                    cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme));
                    cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme));
                    cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme));
                    cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme)); //ovdje bi se trebali remove objekti koji su null sto ne bi smjeli raditi
                }
            }
            else
                throw new InvalidOperationException("Unijeli ste nepoznatu opciju!"); //citav kod ispod else if (opcija == 2) nece se ni desiti
        }

        public void DodajBuket(List<Cvijet> cvijeće, List<string> dodaci, Poklon poklon, double cijena)
        {
            Buket b = new Buket(cijena);
            b = new Buket(0); //stavlja buket na cijenu 0 ne koristi originalnu
            b.DodajPoklon(poklon);
            foreach (Cvijet c in cvijeće)
                b.DodajCvijet(c);
            foreach (Cvijet c in cvijeće) ; //nepotrebna for petlja nista ne radi
            foreach (string dodatak in dodaci)
                b.DODAJDODATAK(dodatak);
        }

        public bool ObrišiBuket(Buket b)
        {
            return buketi.Remove(b);
        }

        public void PregledajCvijeće()
        {
            foreach (Cvijet cvijet in cvijeće)
            {
                cvijet.ProvjeriKrajSezone();
            }
            for (int i = 0; i < cvijeće.Count; i++)
                cvijeće[i].ProvjeriKrajSezone(); // 2 iste for petlje

            cvijeće.RemoveAll(cvijet => cvijet.Kolicina == 0);
        }

        public void NaručiCvijeće(Mušterija m, Buket b, Poklon p)
        {
            if (!buketi.Contains(b))
                throw new InvalidOperationException("Traženi buket nije na stanju!");

            m.RegistrujKupovinu(b, p);
            naručeniPokloni.Add(p);
        }

        #endregion
    }
}
