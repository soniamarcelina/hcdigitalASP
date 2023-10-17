using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hcdigital.Models;
using hcdigital.Data;
using Microsoft.Extensions.Logging.Console;

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
        if (_context.mrf != null && _context.tadposition != null && _context.masteremployee != null )
        {
                var query = from mrf in _context.mrf
                        join position in _context.tadposition on mrf.id_position equals position.id_position
                        join directpos in _context.masteremployee on position.DirectPos_ID equals directpos.ID_Position
                        select new
                        {
                            MRF = mrf,
                            Position = position,
                            DirectPos = directpos
                        };

            var result = query.ToList();
            return View(result);
        } else {
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

     [HttpGet("GetTadPosition")]
    public IActionResult GetTadPosition()
    {
       try
        {
            if (_context.tadposition != null && _context.masteremployee != null && _context.assignmentorder != null && _context.tademployee != null )
            {
                var query = from position in _context.tadposition
                    join directpos in _context.masteremployee on position.DirectPos_ID equals directpos.ID_Position
                    join assign in _context.assignmentorder on position.ID_AO equals assign.id
                    join employee in _context.tademployee on assign.id_personnel equals employee.Nopek
                    select new
                    {
                        Position = position,
                        DirectPos = directpos,
                        Assignment = assign,
                        TKJP = employee
                    };

            var result = query.ToList();
            return Ok(result);

            } else { 
            return Ok(new List<Position>()); 
        }

        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
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
           
        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
    }

     [HttpPost("Insert")]
    public IActionResult Insert([FromBody] MRF data)
    {
        try
        {
                var mrfEntity = new MRF
            {
                mrf_code = data.mrf_code,
                status = data.status,
                // Isi properti lainnya sesuai kebutuhan.
            };
              _context.mrf?.Add(mrfEntity);
             _context.SaveChanges();
            
           
            var response = new
            {
                status = 2, //(2 mungkin mengindikasikan sukses).
                msg = "MRF data saved successfully",
                data = new
                {
                    tempKey = "tempKey"
                }
            };

            return Ok(response);
        }
        catch (Exception ex)
        {

            return BadRequest(new { error = ex.Message });
        }
    }

    public IActionResult SubmitMRF([FromBody] MRF mrf)
    {
        // Konversi ViewModel ke model data yang sesuai
        var data = new MRF
        {
            mrf_code = mrf.mrf_code,
            
            // Set properti lainnya sesuai kebutuhan
        };

        // Menggunakan DbContext untuk menyimpan data
        
            _context.mrf?.Add(data); // Menambahkan data MRF ke DbSet
            _context.SaveChanges(); // Simpan perubahan ke database
        

        return Ok(new { Message = "Data MRF berhasil disimpan" });
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
