using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace hcdigital.Controllers;
[Route("Grade")]
[ApiController]

public class GradeController : Controller
{
   
    private readonly ApplicationDbContext _context;

    public GradeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("Index")]
    public IActionResult Index()
    {
        var grade = _context.grade?.ToList() ?? new List<Grade>();
        return View(grade);
    }


    [HttpGet("GetGrade")]
    public IActionResult GetGrade()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.grade?.ToList() ?? new List<Grade>(); 
            
            // Kirim data sebagai respons JSON
            return Ok(data);
        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
