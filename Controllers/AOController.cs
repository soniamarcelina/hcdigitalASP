using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;
using Microsoft.EntityFrameworkCore;

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

[HttpGet("GetAssignments/{id}")]
public IActionResult GetAssignments(int id)
{
    try
    {
        if (_context.assignmentorder != null && _context.contractor != null && _context.mrf != null)
        {
            var Aorder = _context.assignmentorder
                .Where(a => a.id == id)
                .Join(
                    _context.contractor,
                    a => a.contractor_id,
                    c => c.contractNo,
                    (AO a, Contractor c) => new { Assignment = a, Contractor = c }
                )
                // .Join(
                //     _context.mrf,
                //     m => m.Assignment.mrf_id,
                //     MRF =>  MRF.id_mrf,
                //     (ac, Mrf ) => new {ac.Assignment, ac.Contractor, mrf = Mrf}
                // )
                .SingleOrDefault();

            if (Aorder != null)
            {
                return Json(new { success = true, data = Aorder });
            }
            else
            {
                return Json(new { success = false, message = "Data tidak ditemukan" });
            }
        }

        return Json(new { success = false, message = "Data tidak ditemukan" });
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
