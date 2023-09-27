using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;

public class DirectPosController : Controller
{
    private readonly ApplicationDbContext _context;

    public DirectPosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var dipos = _context.masteremployee?.ToList() ?? new List<DirectPos>();
        return View(dipos);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
