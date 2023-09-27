using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;
[Route("Competency")]
[ApiController]


public class CompetencyController : Controller
{
    private readonly ApplicationDbContext _context;

    public CompetencyController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("Index")]
    public IActionResult Index()
    {
        var comp = _context.technical_comp?.ToList() ?? new List<Competency>();
        return View(comp);
    }

    [HttpGet("GetCompetency")]
    public IActionResult GetCompetency()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.technical_comp?.ToList() ?? new List<Competency>(); 
            
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
