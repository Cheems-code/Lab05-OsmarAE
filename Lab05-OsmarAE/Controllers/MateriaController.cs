using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab05_OsmarAE.Models;
using Lab05_OsmarAE.Repositorios;

namespace Lab05_OsmarAE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MateriasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MateriasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/Materias
    [HttpGet]
    public async Task<IActionResult> GetMaterias()
    {
        var materias = await _unitOfWork.Repository<Materia>().GetAllAsync();
        return Ok(materias);
    }

    // GET: api/Materias/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMateriaById(int id)
    {
        var materia = await _unitOfWork.Repository<Materia>().GetByIdAsync(id);
        if (materia == null)
        {
            return NotFound($"No se encontró la materia con ID {id}.");
        }
        return Ok(materia);
    }

    // POST: api/Materias
    [HttpPost]
    public async Task<IActionResult> CreateMateria(Materia materia)
    {
        await _unitOfWork.Repository<Materia>().AddAsync(materia);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetMateriaById), new { id = materia.IdMateria }, materia);
    }

    // PUT: api/Materias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMateria(int id, Materia materia)
    {
        if (id != materia.IdMateria)
        {
            return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
        }

        _unitOfWork.Repository<Materia>().Update(materia);

        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound($"No se pudo actualizar. La materia con ID {id} ya no existe.");
        }

        return NoContent();
    }

    // DELETE: api/Materias/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMateria(int id)
    {
        var materia = await _unitOfWork.Repository<Materia>().GetByIdAsync(id);
        if (materia == null)
        {
            return NotFound($"No se encontró la materia con ID {id} para eliminar.");
        }

        _unitOfWork.Repository<Materia>().Remove(materia);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}