using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;

namespace hcdigital.Controllers;

public class ApprovalController : Controller
{
    private readonly ApplicationDbContext _context;

    public ApprovalController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var app = _context.Approval?.ToList() ?? new List<Approval>();
        return View(app);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
