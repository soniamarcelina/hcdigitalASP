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
            var result = _context.mrf
                    .Join(
                        _context.tadposition,
                        m => m.id_position,
                        p => p.id_position,
                        (MRF m, Position p) => new { MRF = m, Position = p }
                    )
                    .Join(
                        _context.masteremployee,
                        pm => pm.Position.DirectPos_ID,
                        masteremployee =>  masteremployee.ID_Position,
                        (mp, masteremployee ) => new {mp.MRF, mp.Position, Direct = masteremployee}
                    )
                    .Select(mp => new MRF {
                        id_mrf = mp.MRF.id_mrf,
                        status = mp.MRF.status,
                        mrf_code = mp.MRF.mrf_code,
                        mrf_type = mp.MRF.mrf_type,
                        start_date = mp.MRF.start_date,
                        end_date = mp.MRF.end_date,
                        tempKey = mp.MRF.tempKey,
                        id_position = mp.MRF.id_position,
                        Position = mp.Position,
                        DirectPos = mp.Direct,
                    })
                    .ToList();
                return Ok(result);
            } else {
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
