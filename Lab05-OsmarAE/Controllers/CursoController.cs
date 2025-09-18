using Lab05_OsmarAE.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Lab05_OsmarAE.Controllers;

using Microsoft.AspNetCore.Mvc;
using Lab05_OsmarAE.Models;  

[Route("api/[controller]")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CursoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/Cursos
    [HttpGet]
    public async Task<IActionResult> GetAllCursos()
    {
        var cursos = await _unitOfWork.Repository<Curso>().GetAllAsync();
        return Ok(cursos);
    }

    // GET: api/Cursos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCursoById(int id)
    {
        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (curso == null)
        {
            return NotFound(); 
        }
        return Ok(curso);
    }

    // POST: api/Cursos
    [HttpPost]
    public async Task<IActionResult> CreateCurso(Curso curso)
    {
        await _unitOfWork.Repository<Curso>().AddAsync(curso);
        
        await _unitOfWork.CompleteAsync();
        
        return CreatedAtAction(nameof(GetCursoById), new { id = curso.IdCurso }, curso);
    }

    // PUT: api/Cursos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCurso(int id, Curso curso)
    {
        if (id != curso.IdCurso)
        {
            return BadRequest();
        }

        _unitOfWork.Repository<Curso>().Update(curso);
        
        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }

        return NoContent(); 
    }

    // DELETE: api/Cursos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurso(int id)
    {
        var curso = await _unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (curso == null)
        {
            return NotFound();
        }

        _unitOfWork.Repository<Curso>().Remove(curso);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}