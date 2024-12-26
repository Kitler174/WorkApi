using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkAPI.Models;
using WorkAPI.Services;

namespace WorkAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class MagazynController : ControllerBase
{
    private readonly MagazynServices _serv;
    public MagazynController(MagazynServices serv)
    {
        _serv = serv;
    }

    //Uzyskiwanie magazynów
    [HttpGet("/GetWar")]
    public IActionResult GetWar()
    {
        var lista = _serv.GetWar();
        return Ok(lista);
    }
    [HttpGet("/GetProducts")]
    public IActionResult GetProd()
    {
        var lista = _serv.GetProd();
        return Ok(lista);
    }

}

