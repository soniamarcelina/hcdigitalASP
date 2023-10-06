using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // var manpower = _context.mrf?.ToList() ?? new List<MRF>();
        // return View(manpower);
        if (_context.mrf != null && _context.tadposition != null)
        {
        var result = _context.mrf
            .Join(
                _context.tadposition,
                m => m.id_position,
                p => p.id_position,
                (m, p) => new { Mrf = m, Position = p }
            )
            .Select(mp => new MRF
            {
                id_mrf = mp.Mrf.id_mrf,
                status = mp.Mrf.status,
                mrf_code = mp.Mrf.mrf_code,
                mrf_type = mp.Mrf.mrf_type,
                start_date = mp.Mrf.start_date,
                end_date = mp.Mrf.end_date,
                tempKey = mp.Mrf.tempKey,
                id_position = mp.Mrf.id_position,
                Position = mp.Position,
                
            })
            .ToList();

        return View(result);

        } else
        { 
            return View(new List<MRF>()); 
        }

    }

     [HttpGet("Approval")]
     public IActionResult Approval()
    {
        return View();
    }
     
     [HttpGet("Candidate/{id_mrf}")]
    public IActionResult Candidate(int id_mrf)
    {
        // Dapatkan data employee berdasarkan ID dari database
        MRF? scr = _context.mrf?.Find(id_mrf);

        if (scr == null)
        {
            return NotFound();
        }

        return View(scr);
    }

     [HttpPost]
    public IActionResult InsertCandidate(int id, [FromBody] Candidate cData)
    {
        // Lakukan logika penyisipan kandidat di sini dengan menggunakan data dari cData dan id
        // ...
        
        // Setelah selesai, kembalikan respons yang sesuai
        var response = new { msg = "Candidate inserted successfully" };
        return Json(response);
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

    [HttpGet("GetPosition")]
    public IActionResult GetPosition()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.tadposition?.ToList() ?? new List<Position>(); 
            return Ok(data);
            // Kirim data sebagai respons JSON
            //return Ok(data);
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
