using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using hcdigital.Models;
using hcdigital.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace hcdigital.Controllers;
[Route("Position")]
[ApiController]

public class PositionController : Controller
{
    private readonly ApplicationDbContext _context;

    public PositionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("Index")]
    public IActionResult Index()
    {
        if (_context.tadposition != null && _context.masteremployee != null && _context.assignmentorder != null)
        {
        var result = _context.tadposition
            .Join(
                _context.masteremployee,
                p => p.DirectPos_ID,
                d => d.ID_Position,
                (Position p, DirectPos d) => new { DirectPos = d, Position = p }
            )
            .Join(
                _context.assignmentorder,
                dp => dp.Position.ID_AO,
                assignmentorder => assignmentorder.id,
                (pd, assignmentorder) => new {pd.Position, pd.DirectPos, Assignment = assignmentorder}
            )
            .Join(
                _context.tademployee,
                dpa => dpa.Assignment.id_personnel,
                tademployee => tademployee.Nopek,
                (pd, assignmentorder, tademployee) => new {pd.Position, pd.DirectPos, pd.Assignment, Employee = tademployee}
            )
            .Select(pd => new Position
            {
                id_position = pd.Position.id_position,
                ID_AO = pd.Position.ID_AO,
                PosTitle = pd.Position.PosTitle,
                Direktorat = pd.Position.Direktorat,
                Division = pd.Position.Division,
                Sub_division = pd.Position.Sub_division,
                Department = pd.Position.Department,
                Section = pd.Position.Section,
                Company_ID = pd.Position.Company_ID,
                PersArea_ID = pd.Position.PersArea_ID,
                PersSubArea_ID = pd.Position.PersSubArea_ID,
                CostType = pd.Position.CostType,
                CostCenter = pd.Position.CostCenter,
                Work_Schedule = pd.Position.Work_Schedule,
                Grade = pd.Position.Grade,
                DirectPos_ID = pd.Position.DirectPos_ID,
                DirectPos = pd.DirectPos,
                Assignment = pd.Assignment,
                Employee = pd.Employee
                
            })
            .ToList();

        return View(result);

        } else
        { 
            return View(new List<Position>()); 
        }

    }

    [HttpGet("Search")]
    public IActionResult Search()
    {
        return View();
    }

    [HttpGet("GetPosition")]
    public IActionResult GetPosition()
    {
        try
        {
            // Ambil data dari database 
            var data = _context.masteremployee?.ToList(); 

            // Kirim data sebagai respons JSON
            return Ok(data);
        }
        catch (Exception ex)
        {
            // Tangani kesalahan jika ada
            return BadRequest(new { error = ex.Message });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
