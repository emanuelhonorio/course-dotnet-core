using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Model;
using System.Threading.Tasks;

namespace ProAgil.WebAPI.Controllers
{
  [ApiController]
  [Route("/eventos")]
  public class EventoController : ControllerBase
  {
    private readonly DataContext _DataContext;
    public EventoController(DataContext dataContext)
    {
      this._DataContext = dataContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetEventos()
    {
      List<Evento> eventos = await this._DataContext.Eventos.ToListAsync();
      return Ok(eventos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvento(int id)
    {
      try
      {
        Evento evento = await this._DataContext.Eventos.FirstOrDefaultAsync((el) => el.EventoId == id);
        return Ok(evento);
      }
      catch (System.Exception ex)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, new { message = "We got a problem", stacktrace = ex.Message });
      }
    }
  }
}