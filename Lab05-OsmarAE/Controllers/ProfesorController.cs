using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab05_OsmarAE.Models;
using Lab05_OsmarAE.Repositorios;

namespace Lab05_OsmarAE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfesoresController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfesoresController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/Profesores
    [HttpGet]
    public async Task<IActionResult> GetProfesores()
    {
        var profesores = await _unitOfWork.Repository<Profesore>().GetAllAsync();
        return Ok(profesores);
    }

    // GET: api/Profesores/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfesorById(int id)
    {
        var profesor = await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (profesor == null)
        {
            return NotFound($"No se encontró el profesor con ID {id}.");
        }
        return Ok(profesor);
    }

    // POST: api/Profesores
    [HttpPost]
    public async Task<IActionResult> CreateProfesor(Profesore profesor)
    {
        await _unitOfWork.Repository<Profesore>().AddAsync(profesor);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetProfesorById), new { id = profesor.IdProfesor }, profesor);
    }

    // PUT: api/Profesores/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfesor(int id, Profesore profesor)
    {
        if (id != profesor.IdProfesor)
        {
            return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
        }

        _unitOfWork.Repository<Profesore>().Update(profesor);

        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound($"No se pudo actualizar. El profesor con ID {id} ya no existe.");
        }

        return NoContent();
    }

    // DELETE: api/Profesores/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfesor(int id)
    {
        var profesor = await _unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (profesor == null)
        {
            return NotFound($"No se encontró el profesor con ID {id} para eliminar.");
        }

        _unitOfWork.Repository<Profesore>().Remove(profesor);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}