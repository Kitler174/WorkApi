using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkAPI.Models;
using WorkAPI.Services;

namespace WorkAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class KomunikatorController : ControllerBase
{
    private readonly KomunikatorService _komunikatorService;

    public KomunikatorController(KomunikatorService komunikatorService)
    {
        _komunikatorService = komunikatorService;
    }

    //Uzyskiwanie wszystkich komunikatów u¿ytkownika
    [HttpGet("/GetAll/{odbiorca}")]
    public IActionResult GetKomunikatory(String odbiorca)
    {
        var komunikatory = _komunikatorService.GetAll(odbiorca);
        return Ok(komunikatory);
    }

    //Uzyskiwanie wys³anych komunikatów
    [HttpGet("/GetSended/{nadawca}")]
    public IActionResult GetSended(String nadawca)
    {
        var komunikatory = _komunikatorService.GetSended(nadawca);
        return Ok(komunikatory);
    }

    //Uzyskiwanie przychodz¹cych komunikatów
    [HttpGet("/GetReceived/{odbiorca}")]
    public IActionResult GetReceived(String odbiorca)
    {
        var komunikatory = _komunikatorService.GetReceived(odbiorca);
        return Ok(komunikatory);
    }

    //Uzyskiwanie przychodz¹cych komunikatów
    [HttpGet("/GetAllUnread/{odbiorca}")]
    public IActionResult GetAllUnread(String odbiorca)
    {
        var komunikatory = _komunikatorService.GetAllUnread(odbiorca);
        return Ok(komunikatory);
    }

    //Uzyskiwanie rejestru po id z bazy
    [HttpGet("/GetById/{id}/{odbiorca}")]
    public IActionResult GetKomunikatorById(int id, String odbiorca)
    {
        var komunikator = _komunikatorService.GetById(id, odbiorca);
        if (komunikator == null)
        {
            return NotFound();
        }
        return Ok(komunikator);
    }

    //wstawienie nowego rejestru do bazy
    [HttpPost("/Send/{odbiorcy}")]
    public IActionResult Send(string odbiorcy, [FromBody] Komunikator komunikator)
    {
        var odbiorcyList = odbiorcy.Split(',').ToList();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        foreach (var o in odbiorcyList)
        {
            // Tworzenie nowego obiektu aby by³o inne id
            Komunikator newKomunikator = new Komunikator
            {
                Nadawca = komunikator.Nadawca,
                DataNadania = komunikator.DataNadania,
                CzasNadania = komunikator.CzasNadania,
                Odbiorca = o,
                DataOdebrania = komunikator.DataOdebrania,
                CzasOdebrania = komunikator.CzasOdebrania,
                Tresc = komunikator.Tresc,
                Status = komunikator.Status,
                NadawcaIp = komunikator.NadawcaIp,
                SysDataRej = komunikator.SysDataRej,
                SysOperatorRej = komunikator.SysOperatorRej,
                SysDataMod = komunikator.SysDataMod,
                SysOperatorMod = komunikator.SysOperatorMod,
                Projekt = komunikator.Projekt,
                NrKontr = komunikator.NrKontr,
                Zrodlo = komunikator.Zrodlo,
                IdZrodla = komunikator.IdZrodla,
                Rodzaj = komunikator.Rodzaj,
                Termin = komunikator.Termin,
                Priorytet = komunikator.Priorytet,
                Waznosc = komunikator.Waznosc,
                IdK = komunikator.IdK,
                Uwagi = komunikator.Uwagi,
                Rok = komunikator.Rok,
                IdProjektu = komunikator.IdProjektu
            };
            _komunikatorService.Create(newKomunikator);
        }
        return Ok("Sended");
    }

    //aktualizacja danej status w rejestrze w bazie po id
    [HttpPut("/UpdateMessage/{id}/{odbiorca}")]
    public IActionResult UpdateKomunikatorStatus(int id, String odbiorca)
    {
        // SprawdŸ, czy rekord istnieje
        try
        {
            _komunikatorService.UpdateStatus(id, odbiorca);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}