using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab05_OsmarAE.Models;
using Lab05_OsmarAE.Repositorios;

namespace Lab05_OsmarAE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AsistenciasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AsistenciasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/Asistencias
    [HttpGet]
    public async Task<IActionResult> GetAsistencias()
    {
        var asistencias = await _unitOfWork.Repository<Asistencia>().GetAllAsync();
        return Ok(asistencias);
    }

    // GET: api/Asistencias/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsistenciaById(int id)
    {
        var asistencia = await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (asistencia == null)
        {
            return NotFound($"No se encontró el registro de asistencia con ID {id}.");
        }
        return Ok(asistencia);
    }

    // POST: api/Asistencias
    [HttpPost]
    public async Task<IActionResult> CreateAsistencia(Asistencia asistencia)
    {
        await _unitOfWork.Repository<Asistencia>().AddAsync(asistencia);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetAsistenciaById), new { id = asistencia.IdAsistencia }, asistencia);
    }

    // PUT: api/Asistencias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsistencia(int id, Asistencia asistencia)
    {
        if (id != asistencia.IdAsistencia)
        {
            return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
        }

        _unitOfWork.Repository<Asistencia>().Update(asistencia);

        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound($"No se pudo actualizar. El registro de asistencia con ID {id} ya no existe.");
        }

        return NoContent();
    }

    // DELETE: api/Asistencias/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsistencia(int id)
    {
        var asistencia = await _unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (asistencia == null)
        {
            return NotFound($"No se encontró el registro de asistencia con ID {id} para eliminar.");
        }

        _unitOfWork.Repository<Asistencia>().Remove(asistencia);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}