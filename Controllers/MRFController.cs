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
                        DirectPos = directpos,
                    };

            var result = query.ToList();
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
        if (_context.mrf != null && _context.tadposition != null && _context.masteremployee != null)
        {
       
                var query = from mrf in _context.mrf
                    join position in _context.tadposition on mrf.id_position equals position.id_position
                    join directpos in _context.masteremployee on position.DirectPos_ID equals directpos.ID_Position
                    where mrf.id_mrf == id_mrf
                    select new ViewCand
                    {
                        MRF = mrf,
                        Position = position,
                        DirectPos = directpos
                    };

           var result = query.SingleOrDefault();

            if (result == null)
            {
                return NotFound();
            } else {
                return View(result);
            }
        }
        
        else {
            return NotFound();
        }

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
           
        }
        catch (Exception ex)
        {
            // Tangani kesalahan
            return BadRequest(new { error = ex.Message });
        }
    }

     [HttpPost("Insert")]
    public IActionResult Insert([FromBody] MRF mrf)
    {
        try
        {
                var mrfEntity = new MRF
            {
                id_mrf = mrf.id_mrf,
                mrf_code = mrf.mrf_code,
                yID = mrf.yID,
                status = "Draft",
                mrf_type = mrf.mrf_type,
                id_position = mrf.id_position,
                workTerm = mrf.workTerm,
                ABI_ABO = mrf.ABI_ABO,
                prev_mrf = mrf.prev_mrf,
                justification = mrf.justification,
                start_date = mrf.start_date,
                end_date = mrf.end_date,
                RequestorID = mrf.RequestorID,
                created_by = mrf.created_by,
                tempKey = mrf.tempKey,
                
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

    [HttpPost("SubmitMRF")]
    public IActionResult SubmitMRF([FromBody] MRF mrf)
        {
        if (mrf == null)
        {
            return BadRequest(new { Message = "Data MRF tidak valid" });
        }

        try
        {
            var data = new MRF
            {
                id_mrf = mrf.id_mrf,
                mrf_code = mrf.mrf_code,
                yID = mrf.yID,
                status = "Routing",
                mrf_type = mrf.mrf_type,
                id_position = mrf.id_position,
                workTerm = mrf.workTerm,
                ABI_ABO = mrf.ABI_ABO,
                prev_mrf = mrf.prev_mrf,
                justification = mrf.justification,
                start_date = mrf.start_date,
                end_date = mrf.end_date,
                RequestorID = mrf.RequestorID,
                created_by = mrf.created_by,
                tempKey = mrf.tempKey,

            };

            _context.mrf?.Add(data);
            _context.SaveChanges();

            return Ok(new { Message = "Data MRF berhasil disimpan" });
        }
        catch (Exception ex)
            {
                // Tangani kesalahan dengan baik, misalnya, log pesan kesalahan
                return StatusCode(500, new { Message = "Gagal menyimpan data MRF: " + ex.Message, InnerException = ex.InnerException?.Message });
            }
        }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
