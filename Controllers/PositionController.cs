using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;
using Microsoft.EntityFrameworkCore;


namespace hcdigital.Controllers;
[Route("Position")]
[ApiController]

public class PositionController : Controller
{
    private readonly ApplicationDbContext _context;

    public PositionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("Index")]
    public IActionResult Index()
    {
        var position = _context.tadposition?.ToList() ?? new List<Position>();
        return View(position);

        // var position = _context.tadposition?.Include(i => i.masteremployee).ToList();
        // return View(position);

        // var query = from pos in _context.tadposition
        //     join directPos in _context.masteremployee
        //     on pos.ID_Position equals directPos.ID_Position
        //     where pos != null && directPos != null
        //     select new { pos, directPos };

        // var combinedData = query.ToList();

        // return View(combinedData);

        // var query = from directPos in _context.masteremployee
        //     from pos in _context.tadposition.Where(p => p.ID_Position == directPos.ID_Position).DefaultIfEmpty()
        //     select new { directPos, pos };

        //     var combinedData = query.ToList();

        //     return View(combinedData);
        
     }

    [HttpGet("Search")]
    public IActionResult Search()
    {
        return View();
    }

    [HttpGet("GetPosition")]
    public IActionResult GetPosition()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.masteremployee?.ToList(); 

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
