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
        var manpower = _context.mrf?.ToList() ?? new List<MRF>();
        return View(manpower);
    //    var mrfData = _context.mrf?.Include(m => m.Position).ToList();
    //     return View(mrfData);
        //     var mrfPositionData = (from mrf in _context.mrf
        //                   join tadposition in _context.tadposition on mrf.id_position equals tadposition.Id_Position
        //                   where mrf != null
        //                   select new MRF
        //                     {
        //                         Position = tadposition
                               
        //                     }).ToList();

        // return View(mrfPositionData);
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
