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
