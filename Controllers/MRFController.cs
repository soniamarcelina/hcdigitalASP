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
        var result = _context.mrf?.ToList() ?? new List<MRF>();
        return View(result);
        // if (_context.mrf != null && _context.tadposition != null && _context.masteremployee != null)
        // {
        // var result = _context.mrf
        //     .Join(
        //         _context.tadposition,
        //         m => m.id_position,
        //         p => p.id_position,
        //         (m, p) => new { Mrf = m, Position = p }
        //     )
            // .Join(
            //     _context.masteremployee,
            //     pm => pm.Position.DirectPos_ID,
            //     masteremployee => masteremployee.ID_Position,
            //     (mp, masteremployee) => new {mp.Mrf, mp.Position, DirectPos = masteremployee }
            // )
        //     .Select(mp => new MRF
        //     {
        //         id_mrf = mp.Mrf.id_mrf,
        //         status = mp.Mrf.status,
        //         mrf_code = mp.Mrf.mrf_code,
        //         mrf_type = mp.Mrf.mrf_type,
        //         start_date = mp.Mrf.start_date,
        //         end_date = mp.Mrf.end_date,
        //         tempKey = mp.Mrf.tempKey,
        //         created_at = mp.Mrf.created_at,
        //         id_position = mp.Mrf.id_position,
        //         Position = mp.Position,
        //         //DirectPos = mp.DirectPos,
                
        //     })
        //     .ToList();

        // return View(result);

        // } else
        // { 
        //     return View(new List<MRF>()); 
        // }

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

     [HttpGet("GetTADPosition")]
    public IActionResult GetTADPosition()
    {
       try
        {
            if (_context.tadposition != null && _context.masteremployee != null && _context.assignmentorder != null && _context.tademployee != null )
            {
            var result = _context.tadposition
            .Join(
                _context.masteremployee,
                p => p.DirectPos_ID,
                d => d.ID_Position,
                (Position p, DirectPos d) => new { DirectPos = d, Position = p }
            )
            .Join(
                _context.assignmentorder,
                dp => dp.Position.ID_AO,
                assignmentorder => assignmentorder.id,
                (pd, assignmentorder) => new {pd.Position, pd.DirectPos, Assignment = assignmentorder}
            ).Join(
                _context.tademployee, 
                ta => ta.Assignment.id_personnel,
                tademployee => tademployee.Nopek,
                (pd, tademployee) => new {pd.Position, pd.DirectPos, pd.Assignment, Employee = tademployee}
            )
            .Select(pd => new Position
            {
                id_position = pd.Position.id_position,
                ID_AO = pd.Position.ID_AO,
                PosTitle = pd.Position.PosTitle,
                Direktorat = pd.Position.Direktorat,
                Division = pd.Position.Division,
                Sub_division = pd.Position.Sub_division,
                Department = pd.Position.Department,
                Section = pd.Position.Section,
                Company_ID = pd.Position.Company_ID,
                PersArea_ID = pd.Position.PersArea_ID,
                PersSubArea_ID = pd.Position.PersSubArea_ID,
                CostType = pd.Position.CostType,
                CostCenter = pd.Position.CostCenter,
                Work_Schedule = pd.Position.Work_Schedule,
                Grade = pd.Position.Grade,
                DirectPos_ID = pd.Position.DirectPos_ID,
                DirectPos = pd.DirectPos,
                Assignment = pd.Assignment,
                Employee = pd.Employee,
            
            })
            .ToList();

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
    public IActionResult Insert(MRF mrf)
    {
        try
        {
            _context.mrf?.Add(mrf);
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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
