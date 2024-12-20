using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkAPI.Models;
using WorkAPI.Services;

namespace WorkAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ZamowieniaController : ControllerBase
{
    private readonly ZamowieniaService _zamowieniaService;

    public ZamowieniaController(ZamowieniaService zamowieniaService)
    {
        _zamowieniaService = zamowieniaService;
    }

    //Uzyskiwanie danych o handlowcu
    [HttpGet("/GetSeller/{odbiorca}")]
    public IActionResult GetSeller(String odbiorca)
    {
        var handlowiec = _zamowieniaService.GetSeller(odbiorca);
        return Ok(handlowiec);
    }

    //Uzyskiwanie kontrahenta
    [HttpGet("/GetContractor/{odbiorca}")]
    public IActionResult GetContractor(String odbiorca)
    {
        var kontrahent = _zamowieniaService.GetContrator(odbiorca);
        return Ok(kontrahent);
    }

    // Uzyskiwanie wszelkich informacji o magazynie
    [HttpGet("/GetWarehouse")]
    public IActionResult GetWarehouse()
    {
        var magazyn = _zamowieniaService.GetWarehouse();
        return Ok(magazyn);
    }

    // Uzyskiwanie zamówienień kupca
    [HttpGet("/GetOrders/{odbiorca}")]
    public IActionResult GetOrders(String odbiorca)
    {
        var zamowienia = _zamowieniaService.GetOrders(odbiorca);
        return Ok(zamowienia);
    }

    // Uzyskiwanie zamówienia kupca
    [HttpGet("/GetItems/{NumerZamowienia}")]
    public IActionResult GetItems(String NumerZamowienia)
    {
        var zamowienie = _zamowieniaService.GetItems(NumerZamowienia);
        return Ok(zamowienie);
    }
    [HttpGet("/GetParams")]
    public IActionResult GetParams()
    {
        var par = _zamowieniaService.GetParams();
        return Ok(par);
    }
    [HttpGet("/GetFiltered")]
    public IActionResult GetFiltered([FromQuery] String? nazwa, [FromQuery] List<String> categoryIds, [FromQuery] List<List<String>> categorypar)
    {
        var prod = _zamowieniaService.GetFiltered(categoryIds,categorypar,nazwa);
        return Ok(prod);
    }

    //wstawienie nowego rejestru do bazy
    [HttpPost("/SendOrder")]
    public IActionResult SendOrder([FromBody] Zamowienie zamowienia)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _zamowieniaService.Create(zamowienia);
        return Ok("Zamówienie wysłane");
    }

    [HttpDelete("/DelPosition/{numerZamowienia}/{pozycja}")]
    public async Task<IActionResult> DeletePosition(string numerZamowienia, int pozycja)
    {
        _zamowieniaService.DelPosition(numerZamowienia, pozycja);
        return Ok();
    }

    [HttpDelete("/DelOrder/{numerZamowienia}")]
    public async Task<IActionResult> DeleteOrder(string numerZamowienia)
    {
        _zamowieniaService.DelOrder(numerZamowienia);
        return Ok();
    }

    [HttpPost("/EditPosition/{numerZamowienia}/{pozycja}/{ilosc}")]
    public async Task<IActionResult> EditPosition(string numerZamowienia, int pozycja, int ilosc)
    {
        _zamowieniaService.EditPosition(numerZamowienia, pozycja, ilosc);
        return Ok();
    }

    [HttpPost("/AddPosition/{numerZamowienia}")]
    public async Task<IActionResult> AddPosition(string numerZamowienia, [FromBody] Zamowienie_pozycja poz)
    {
        _zamowieniaService.AddPosition(numerZamowienia, poz);
        return Ok();
    }
}