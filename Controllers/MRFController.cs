using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;
[Route("MRF")]
[ApiController]

public class MRFController : Controller
{
    private readonly ApplicationDbContext _context;

    public MRFController(ApplicationDbContext context)
    {
        _context = context;
    }

     [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

     [HttpGet("Index")]
    public IActionResult Index()
    {
        var manpower = _context.mrf?.ToList() ?? new List<MRF>();
        return View(manpower);
    }

     [HttpGet("Approval")]
     public IActionResult Approval()
    {
        return View();
    }
     
     [HttpGet("Candidate")]
      public IActionResult Candidate()
    {
        return View();
    }

    [HttpGet("GetCandidate")]
    public IActionResult GetCandidate()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.tademployee?.ToList() ?? new List<TKJP>(); 

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
