using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab05_OsmarAE.Models;
using Lab05_OsmarAE.Repositorios;

namespace Lab05_OsmarAE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EvaluacionesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EvaluacionesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/Evaluaciones
    [HttpGet]
    public async Task<IActionResult> GetEvaluaciones()
    {
        var evaluaciones = await _unitOfWork.Repository<Evaluacione>().GetAllAsync();
        return Ok(evaluaciones);
    }

    // GET: api/Evaluaciones/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvaluacionById(int id)
    {
        var evaluacion = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (evaluacion == null)
        {
            return NotFound($"No se encontró la evaluación con ID {id}.");
        }
        return Ok(evaluacion);
    }

    // POST: api/Evaluaciones
    [HttpPost]
    public async Task<IActionResult> CreateEvaluacion(Evaluacione evaluacion)
    {
        await _unitOfWork.Repository<Evaluacione>().AddAsync(evaluacion);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetEvaluacionById), new { id = evaluacion.IdEvaluacion }, evaluacion);
    }

    // PUT: api/Evaluaciones/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvaluacion(int id, Evaluacione evaluacion)
    {
        if (id != evaluacion.IdEvaluacion)
        {
            return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
        }

        _unitOfWork.Repository<Evaluacione>().Update(evaluacion);

        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound($"No se pudo actualizar. La evaluación con ID {id} ya no existe.");
        }

        return NoContent();
    }

    // DELETE: api/Evaluaciones/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvaluacion(int id)
    {
        var evaluacion = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (evaluacion == null)
        {
            return NotFound($"No se encontró la evaluación con ID {id} para eliminar.");
        }

        _unitOfWork.Repository<Evaluacione>().Remove(evaluacion);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}