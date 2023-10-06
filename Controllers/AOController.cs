using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;
[Route("AO")]
[ApiController]

public class AOController : Controller
{
    private readonly ApplicationDbContext _context;

    public AOController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("Index")]
    public IActionResult Index()
    {
        var Aorder = _context.assignmentorder?.ToList() ?? new List<AO>();
        return View(Aorder);
    }

    [HttpGet("Interview")]
    public IActionResult Interview()
    {
        return View();
    }

    [HttpGet("Offering")]
    public IActionResult Offering()
    {
        return View();
    }

    [HttpGet("Approval")]
    public IActionResult Approval()
    {
        return View();
    }

    [HttpGet("GetAssignmentById/{id}")]
    public JsonResult GetAssignmentById(int id)
    {
        try
        {
            var Aorder = _context.assignmentorder?.SingleOrDefault(a => a.id == id);

            if (Aorder != null)
            {
                return Json(new { success = true, data = Aorder });
            }
            else
            {
                return Json(new { success = false, message = "Data tidak ditemukan" });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Terjadi kesalahan: " + ex.Message });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
