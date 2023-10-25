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

     [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpGet("Offering")]
    public IActionResult Offering()
    {
        return View();
    }

    [HttpGet("Assign")]
    public IActionResult Assign()
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

    [HttpGet("GetMRF")]
    public IActionResult GetMRF()
       {
          try
        {
            if (_context.mrf != null && _context.tadposition != null && _context.masteremployee != null)
            {
                var query = from mrf in _context.mrf
                    join position in _context.tadposition on mrf.id_position equals position.id_position
                    join directpos in _context.masteremployee on position.DirectPos_ID equals directpos.ID_Position
                    select new
                    {
                        MRF = mrf,
                        Position = position,
                        DirectPos = directpos,
                    };

                var result = query.ToList();
                return Ok(result);

                } else
                { 
                    return Ok(new List<MRF>()); 
                }

            }
            catch (Exception ex)
            {
                // Tangani kesalahan jika ada
                return BadRequest(new { error = ex.Message });
            }
       }

    [HttpGet("GetTKJP")]
    public IActionResult GetTKJP()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.tademployee?.ToList() ?? new List<TKJP>(); 
            return Ok(data);
            
        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
    }

     [HttpGet("GetContractor")]
    public IActionResult GetContractor()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.contractor?.ToList() ?? new List<Contractor>(); 
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
