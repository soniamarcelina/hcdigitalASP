using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;

public class ContractorController : Controller
{
   private readonly ApplicationDbContext _context;

    public ContractorController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var contract = _context.contractor?.ToList() ?? new List<Contractor>();
        return View(contract);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Contractor contractor)
    {
        if (ModelState.IsValid)
        {
            _context.contractor?.Add(contractor);
            _context.SaveChanges();
            return RedirectToAction("Index", "Contractor"); // Ganti "Index" dengan tindakan yang sesuai.
        }

        return View(contractor);
    }

    [Route("Contractor/Add")]
    public IActionResult Add()
    {
        return View();
    }

    [Route("Contractor/Edit/{contractNo}")]
   public IActionResult Edit(string contractNo)
    {
        // Dapatkan data kontraktor berdasarkan ID dari database
        Contractor? contract = _context.contractor?.Find(contractNo);

        if (contract == null)
        {
            return NotFound();
        }

        return View(contract);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Contractor contractor)
    {
        if (ModelState.IsValid)
        {
            // Simpan perubahan ke dalam database
            _context.Update(contractor);
            _context.SaveChanges();
            
            return RedirectToAction("Index"); // Arahkan pengguna kembali ke halaman utama
        }

        return View(contractor);
    }

    [Route("Contractor/Delete/{contractNo}")]
    public IActionResult Delete(string contractNo)
    {
        // Dapatkan data kontraktor berdasarkan ID dari database
        Contractor? contractor = _context.contractor?.Find(contractNo);

        if (contractor == null)
        {
            return NotFound();
        }

        _context.contractor?.Remove(contractor);
        _context.SaveChanges();

        return RedirectToAction("Index"); // Redirect ke halaman utama atau tampilan daftar data.
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
