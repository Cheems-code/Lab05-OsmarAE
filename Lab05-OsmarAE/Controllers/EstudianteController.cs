using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Lab05_OsmarAE.Models;
using Lab05_OsmarAE.Repositorios;

namespace Lab05_OsmarAE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstudiantesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EstudiantesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/Estudiantes
    [HttpGet]
    public async Task<IActionResult> GetEstudiantes()
    {
        // Pide el repositorio para <Estudiante> y obtiene todos los registros.
        var estudiantes = await _unitOfWork.Repository<Estudiante>().GetAllAsync();
        return Ok(estudiantes);
    }

    // GET: api/Estudiantes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEstudianteById(int id)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null)
        {
            return NotFound($"No se encontró el estudiante con ID {id}.");
        }
        return Ok(estudiante);
    }

    // POST: api/Estudiantes
    [HttpPost]
    public async Task<IActionResult> CreateEstudiante(Estudiante estudiante)
    {
        await _unitOfWork.Repository<Estudiante>().AddAsync(estudiante);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetEstudianteById), new { id = estudiante.IdEstudiante }, estudiante);
    }

    // PUT: api/Estudiantes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEstudiante(int id, Estudiante estudiante)
    {
        if (id != estudiante.IdEstudiante)
        {
            return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
        }

        _unitOfWork.Repository<Estudiante>().Update(estudiante);

        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound($"No se pudo actualizar. El estudiante con ID {id} ya no existe.");
        }

        return NoContent(); 
    }

    // DELETE: api/Estudiantes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstudiante(int id)
    {
        var estudiante = await _unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (estudiante == null)
        {
            return NotFound($"No se encontró el estudiante con ID {id} para eliminar.");
        }

        _unitOfWork.Repository<Estudiante>().Remove(estudiante);
        await _unitOfWork.CompleteAsync();

        return NoContent(); 
    }
}