using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;
using System.Linq;
using System.Collections.Generic;
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
        if (_context.tadposition != null && _context.masteremployee != null && _context.assignmentorder != null && _context.tademployee != null )
        {
            var query = from position in _context.tadposition
                    join directpos in _context.masteremployee on position.DirectPos_ID equals directpos.ID_Position
                    join assign in _context.assignmentorder on position.ID_AO equals assign.id
                    join employee in _context.tademployee on assign.id_personnel equals employee.Nopek
                    select new
                    {
                        Position = position,
                        DirectPos = directpos,
                        Assignment = assign,
                        TKJP = employee
                    };

            var result = query.ToList();
            return View(result);

        } else
        { 
            return View(new List<Position>()); 
        }

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
