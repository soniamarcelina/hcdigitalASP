using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;

public class ApprovalController : Controller
{
    private readonly ApplicationDbContext _context;

    public ApprovalController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var app = _context.Approval?.ToList() ?? new List<Approval>();
        return View(app);
    }

    [HttpGet("GetApproval/{id}")]
    public IActionResult GetApproval(int id)
     {
        try
        {
            // Ambil data dari database 
            var data = _context.Approval?.ToList() ?? new List<Approval>(); 
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
