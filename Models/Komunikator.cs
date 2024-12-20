namespace WorkAPI.Models
{
    public class Komunikator
    {
        public string? Nadawca { get; set; }
        public DateTime? DataNadania { get; set; }
        public TimeSpan? CzasNadania { get; set; }
        public string? Odbiorca { get; set; }
        public DateTime? DataOdebrania { get; set; }
        public TimeSpan? CzasOdebrania { get; set; }
        public string? Tresc { get; set; }
        public string? Status { get; set; }
        public int Id { get; private set; }
        public string? NadawcaIp { get; set; }
        public DateTime? SysDataRej { get; set; }
        public string? SysOperatorRej { get; set; }
        public DateTime? SysDataMod { get; set; }
        public string? SysOperatorMod { get; set; }
        public string? Projekt { get; set; }
        public string? NrKontr { get; set; }
        public string? Zrodlo { get; set; }
        public int? IdZrodla { get; set; }
        public string? Rodzaj { get; set; }
        public DateTime? Termin { get; set; }
        public short? Priorytet { get; set; }
        public DateTime? Waznosc { get; set; }
        public int? IdK { get; set; }
        public string? Uwagi { get; set; }
        public short? Rok { get; set; }
        public int? IdProjektu { get; set; }
    }
}