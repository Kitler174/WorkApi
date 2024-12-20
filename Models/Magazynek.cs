using System;

namespace WorkAPI.Models
{
    public class Magazynek
    {
        public int Id { get; set; }               // Kolumna 'Id'
        public string nr_magazynu { get; set; }    // Kolumna 'nr_magazynu'
        public string indeks { get; set; }         // Kolumna 'indeks'
        public string nazwa1 { get; set; }         // Kolumna 'nazwa1'
        public string rodzaj_dokum { get; set; }   // Kolumna 'rodzaj_dokum'
        public decimal ilosc { get; set; }         // Kolumna 'ilosc'
        public string jedn { get; set; }           // Kolumna 'jedn'
        public decimal cena_jedn { get; set; }     // Kolumna 'cena_jedn'
        public decimal wartosc { get; set; }       // Kolumna 'wartosc'
        public string waluta_kod { get; set; }     // Kolumna 'waluta_kod'
        public string nr_zamowienia { get; set; }  // Kolumna 'nr_zamowienia'
        public string poz_zamow { get; set; }      // Kolumna 'poz_zamow'
        public string nr_zlecenia { get; set; }    // Kolumna 'nr_zlecenia'
        public string nr_dostawcy { get; set; }    // Kolumna 'nr_dostawcy'
        public string nr_odbiorcy { get; set; }    // Kolumna 'nr_odbiorcy'
        public string status_dokum { get; set; }   // Kolumna 'status_dokum'
        public string uwagi { get; set; }          // Kolumna 'uwagi'
        public DateTime data_rej { get; set; }     // Kolumna 'data_rej'
        public string operator_rej { get; set; }   // Kolumna 'operator_rej'
    }
}
