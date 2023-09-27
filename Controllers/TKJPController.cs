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

    public IActionResult Search()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
