using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;
[Route("OCF")]
[ApiController]

public class OCFController : Controller
{
    private readonly ApplicationDbContext _context;

    public OCFController(ApplicationDbContext context)
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
        var ocf = _context.ocf?.ToList() ?? new List<OCF>();
        return View(ocf);
    }

    
     public IActionResult RequestOrg()
    {
        return View();
    }

    [HttpGet("Approval")]
    public IActionResult Approval()
    {
        return View();
    }

     [HttpGet("GetPosition")]
    public IActionResult GetPosition()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.masteremployee?.ToList() ?? new List<DirectPos>(); 
            return Ok(data);
            
        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
    }

     [HttpGet("GetRoles")]
    public IActionResult GetRoles()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.jobroles?.ToList() ?? new List<JobRoles>(); 

            // Kirim data sebagai respons JSON
            return Ok(data);
        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("Submit")]
    public IActionResult Submit([FromBody] OCF ocf)
    {
        try
        {
             _context.ocf?.Add(ocf);
            _context.SaveChanges();

            // Response berhasil
            var response = new
            {
                status = 2,
                msg = "Data berhasil disimpan."
            };
            return Json(response);
        }
        catch (Exception ex)
        {
            // Response gagal
            var response = new
            {
                status = 1,
                msg = "Terjadi kesalahan saat menyimpan data: " + ex.Message
            };
            return Json(response);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
