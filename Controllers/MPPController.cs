using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace hcdigital.Controllers;
[Route("MPP")]
[ApiController]
public class MPPController : Controller
{
   
    private readonly ApplicationDbContext _context;

    public MPPController(ApplicationDbContext context)
    {
        _context = context;
    }

     [HttpGet("Index")]
    public IActionResult Index()
    {
    //    string query = "SELECT Division, Department, PosTitle, id_position FROM tadposition"; // Ganti dengan query SQL Anda
    //     if (_context.tadposition != null)
    //     {
    //         List<Position> results = _context.tadposition.FromSqlRaw(query).ToList();
    //         return View(results);
    //     }
    //     else
    //     {
    //         // Handle ketika _context.tadposition bernilai null
    //         // Misalnya, arahkan ke halaman kesalahan atau tindakan lain sesuai kebutuhan.
    //         return View("Error");
    //     }
    var groupedData = _context.tadposition?
    .GroupBy(item => item.Division)
    .Select(group => new
    {
        Division = group.Key,
        Items = group.ToList()
    })
    .ToList();
        return View(groupedData);
    
    }

    [HttpGet("Existing")]
    public IActionResult Existing()
    {
        var results = _context.tadposition?
        .GroupBy(m => new { m.Division, m.Department, m.PosTitle })
        .Select(group => new
        {
            Division = group.Key.Division,
            Department = group.Key.Department,
            PosTitle = group.Key.PosTitle,
            Count = group.Count()
        })
        .ToList();
        return View(results);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
