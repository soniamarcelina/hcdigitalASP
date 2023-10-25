using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;
[Route("OCF")]
[ApiController]

public class OCFController : Controller
{
    private readonly ApplicationDbContext _context;

    public OCFController(ApplicationDbContext context)
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
        var ocf = _context.ocf?.ToList() ?? new List<OCF>();
        return View(ocf);
    }

    
     public IActionResult RequestOrg()
    {
        return View();
    }

    [HttpGet("Approval")]
    public IActionResult Approval()
    {
        return View();
    }

     [HttpGet("GetPosition")]
    public IActionResult GetPosition()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.masteremployee?.ToList() ?? new List<DirectPos>(); 
            return Ok(data);
            
        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
    }

     [HttpGet("GetRoles")]
    public IActionResult GetRoles()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.jobroles?.ToList() ?? new List<JobRoles>(); 

            // Kirim data sebagai respons JSON
            return Ok(data);
        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
    }

     [HttpPost("SubmitOCF")]
    public IActionResult SubmitOCF([FromBody] OCF ocf)
        {
        if (ocf == null)
        {
            return BadRequest(new { Message = "Data OCF tidak valid" });
        }

        try
        {
            var data = new OCF
            {
                id = ocf.id,
                Code = ocf.Code,
                subject = ocf.subject,
                status = "Routing",
                yID = ocf.yID,
                DirectPos = ocf.DirectPos,
                PosTitle = ocf.PosTitle,
                Headcount = ocf.Headcount,
                Grade = ocf.Grade,
                WorkLoc = ocf.WorkLoc,
                WorkSch = ocf.WorkSch,
                WorkCity = ocf.WorkCity,
                RequestorID = ocf.RequestorID,
                costType = ocf.costType,
                costCenter = ocf.costCenter,
                Justification = ocf.Justification,
                tempKey = ocf.tempKey,
                CreatorID = ocf.CreatorID,
                Responsibility = ocf.Responsibility,
                Qualification = ocf.Qualification,
                Competency = ocf.Competency,
                Education = ocf.Education,
                Experience = ocf.Experience,
                Skill = ocf.Skill

            };

            _context.ocf?.Add(data);
            _context.SaveChanges();

            return Ok(new { Message = "Data OCF berhasil disimpan" });
        }
        catch (Exception ex)
            {
                // Tangani kesalahan dengan baik, misalnya, log pesan kesalahan
                return StatusCode(500, new { Message = "Gagal menyimpan data OCF: " + ex.Message, InnerException = ex.InnerException?.Message });
            }
        }

    [HttpGet("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        // Dapatkan data employee berdasarkan ID dari database
        OCF? ocf = _context.ocf?.Find(id);

        if (ocf == null)
        {
            return NotFound();
        }

        _context.ocf?.Remove(ocf);
        _context.SaveChanges();

        return RedirectToAction("Index"); // Redirect ke halaman utama atau tampilan daftar data.
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
