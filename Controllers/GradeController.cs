using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace hcdigital.Controllers;

public class GradeController : Controller
{
   
    private readonly ApplicationDbContext _context;

    public GradeController(ApplicationDbContext context)
    {
        _context = context;
    }


    public IActionResult Index()
    {
        var grade = _context.grade?.ToList() ?? new List<Grade>();
        return View(grade);
    }


    public IActionResult GetGrade()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.grade?.ToList() ?? new List<Grade>(); 
            
            // Kirim data sebagai respons JSON
            return Ok(data);
        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
    }


    public IActionResult Insert(Grade grade)
    {
        if (ModelState.IsValid)
        {
            _context.grade?.Add(grade);
            _context.SaveChanges();
            return RedirectToAction("Index", "Grade"); 
        }

        return View(grade);
    }

    [Route("Grade/Add")]
    public IActionResult Add()
    {
        return View();
    }

    [Route("Grade/Edit/{id}")]
   public IActionResult Edit(string id)
    {
        // Dapatkan data berdasarkan ID dari database
        Grade? grade = _context.grade?.Find(id);

        if (grade == null)
        {
            return NotFound();
        }

        return View(grade);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Grade grade)
    {
        if (ModelState.IsValid)
        {
            // Simpan perubahan ke dalam database
            _context.Update(grade);
            _context.SaveChanges();
            
            return RedirectToAction("Index"); // Arahkan pengguna kembali ke halaman utama
        }

        return View(grade);
    }

    [Route("Grade/Delete/{id}")]
    public IActionResult Delete(string id)
    {
        // Dapatkan data  berdasarkan ID dari database
        Grade? rank = _context.grade?.Find(id);

        if (rank == null)
        {
            return NotFound();
        }

        _context.grade?.Remove(rank);
        _context.SaveChanges();

        return RedirectToAction("Index"); // Redirect ke halaman utama atau tampilan daftar data.
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
