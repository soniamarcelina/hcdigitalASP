using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace hcdigital.Controllers;
[Route("MPP")]
[ApiController]
public class MPPController : Controller
{
   
    private readonly ApplicationDbContext _context;

    public MPPController(ApplicationDbContext context)
    {
        _context = context;
    }

     [HttpGet("Index")]
    public IActionResult Index()
    {
        var res = _context.tadposition?
        .GroupBy(m => new { m.Division, m.Department })
        .Select(group => new
        {
            Division = group.Key.Division,
            Department = group.Key.Department,
            Count = group.Count()
        })
        .ToList();
        return View(res);
    
    }

     [HttpGet("Existing")]
    public IActionResult Existing(string Department)
    {
       var positionsByDepartment = _context.tadposition?
        .Where(p => p.Department == Department)
        .Select(p => p.PosTitle)
        .ToList();

        ViewBag.Department = Department;

        return View(positionsByDepartment);

        // var positions = _context.tadposition?
        //     .GroupBy(m => new {m.Department, m.PosTitle})
        //     .Select(group => new
        //     {
        //         Department = group.Key.Department,
        //         PosTitle = group.Key.PosTitle
        //     })
        //     .ToList();

        // return View(positions);

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
