using Lab05_OsmarAE.Models;
using Lab05_OsmarAE.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab05_OsmarAE.Models;

[Route("api/[controller]")]
[ApiController]
public class MatriculasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MatriculasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/Matriculas
    [HttpGet]
    public async Task<IActionResult> GetMatriculas()
    {
        var matriculas = await _unitOfWork.Repository<Matricula>().GetAllAsync();
        return Ok(matriculas);
    }

    // GET: api/Matriculas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMatriculaById(int id)
    {
        var matricula = await _unitOfWork.Repository<Matricula>().GetByIdAsync(id);
        if (matricula == null)
        {
            return NotFound($"No se encontró la matrícula con ID {id}.");
        }
        return Ok(matricula);
    }

    // POST: api/Matriculas
    [HttpPost]
    public async Task<IActionResult> CreateMatricula(Matricula matricula)
    {
        await _unitOfWork.Repository<Matricula>().AddAsync(matricula);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetMatriculaById), new { id = matricula.IdMatricula }, matricula);
    }

    // PUT: api/Matriculas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMatricula(int id, Matricula matricula)
    {
        if (id != matricula.IdMatricula)
        {
            return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
        }

        _unitOfWork.Repository<Matricula>().Update(matricula);

        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound($"No se pudo actualizar. La matrícula con ID {id} ya no existe.");
        }

        return NoContent();
    }

    // DELETE: api/Matriculas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMatricula(int id)
    {
        var matricula = await _unitOfWork.Repository<Matricula>().GetByIdAsync(id);
        if (matricula == null)
        {
            return NotFound($"No se encontró la matrícula con ID {id} para eliminar.");
        }

        _unitOfWork.Repository<Matricula>().Remove(matricula);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}