using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;

public class TKJPController : Controller
{
     private readonly ApplicationDbContext _context;

    public TKJPController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var employee = _context.tademployee?.ToList() ?? new List<TKJP>();
        return View(employee);
    }

    public IActionResult Insert(TKJP tademployee)
    {
        if (ModelState.IsValid)
        {
            _context.tademployee?.Add(tademployee);
            _context.SaveChanges();
            return RedirectToAction("Index", "TKJP"); // Ganti "Index" dengan tindakan yang sesuai.
        }

        return View(tademployee);
    }

    [Route("TKJP/Add")]
    public IActionResult Add()
    {
        return View();
    }

    [Route("TKJP/Edit/{Nopek}")]
   public IActionResult Edit(int Nopek)
    {
        // Dapatkan data employee berdasarkan ID dari database
        TKJP? emp = _context.tademployee?.Find(Nopek);

        if (emp == null)
        {
            return NotFound();
        }

        return View(emp);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(TKJP tademployee)
    {
        if (ModelState.IsValid)
        {
            // Simpan perubahan ke dalam database
            _context.Update(tademployee);
            _context.SaveChanges();
            
            return RedirectToAction("Index"); // Arahkan pengguna kembali ke halaman utama
        }

        return View(tademployee);
    }

    [Route("TKJP/Delete/{Nopek}")]
    public IActionResult Delete(int Nopek)
    {
        // Dapatkan data employee berdasarkan ID dari database
        TKJP? employee = _context.tademployee?.Find(Nopek);

        if (employee == null)
        {
            return NotFound();
        }

        _context.tademployee?.Remove(employee);
        _context.SaveChanges();

        return RedirectToAction("Index"); // Redirect ke halaman utama atau tampilan daftar data.
    }

    public IActionResult Search()
    {
        return View();
    }

    public ActionResult SearchData(string searchName, int searchID)
    {
        // Panggil metode GetFilter dari DbContext untuk melakukan pencarian
        var query = _context.GetFilter(searchName, searchID);

        // Ambil hasil pencarian
        var result = query.ToList();

        // Kembalikan hasil ke tampilan
        return View("Search", result);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
