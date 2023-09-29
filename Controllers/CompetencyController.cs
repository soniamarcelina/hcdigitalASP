using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;



public class CompetencyController : Controller
{
    private readonly ApplicationDbContext _context;

    public CompetencyController(ApplicationDbContext context)
    {
        _context = context;
    }


    public IActionResult Index()
    {
        var comp = _context.technical_comp?.ToList() ?? new List<Competency>();
        return View(comp);
    }

    
    public IActionResult GetCompetency()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.technical_comp?.ToList() ?? new List<Competency>(); 
            
            // Kirim data sebagai respons JSON
            return Ok(data);
        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
    }

     public IActionResult Insert(Competency competency)
    {
        if (ModelState.IsValid)
        {
            _context.technical_comp?.Add(competency);
            _context.SaveChanges();
            return RedirectToAction("Index", "Competency"); // Ganti "Index" dengan tindakan yang sesuai.
        }

        return View(competency);
    }

    [Route("Competency/Add")]
    public IActionResult Add()
    {
        return View();
    }

    [Route("Competency/Edit/{id}")]
   public IActionResult Edit(int id)
    {
        // Dapatkan data berdasarkan ID dari database
        Competency? comp = _context.technical_comp?.Find(id);

        if (comp == null)
        {
            return NotFound();
        }

        return View(comp);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Competency competency)
    {
        if (ModelState.IsValid)
        {
            // Simpan perubahan ke dalam database
            _context.Update(competency);
            _context.SaveChanges();
            
            return RedirectToAction("Index"); // Arahkan pengguna kembali ke halaman utama
        }

        return View(competency);
    }

    [Route("Competency/Delete/{id}")]
    public IActionResult Delete(int id)
    {
        // Dapatkan data  berdasarkan ID dari database
        Competency? comp = _context.technical_comp?.Find(id);

        if (comp == null)
        {
            return NotFound();
        }

        _context.technical_comp?.Remove(comp);
        _context.SaveChanges();

        return RedirectToAction("Index"); // Redirect ke halaman utama atau tampilan daftar data.
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
