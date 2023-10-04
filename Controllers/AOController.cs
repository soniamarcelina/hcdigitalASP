using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;

public class AOController : Controller
{
    private readonly ApplicationDbContext _context;

    public AOController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var Aorder = _context.assignmentorder?.ToList() ?? new List<AO>();
        return View(Aorder);
    }

    [Route("AO/Interview")]
    public IActionResult Interview()
    {
        return View();
    }

    [Route("AO/Offering")]
    public IActionResult Offering()
    {
        return View();
    }

    public IActionResult Approval()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
