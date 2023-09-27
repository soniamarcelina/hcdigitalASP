using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hcdigital.Data;

namespace hcdigital.Models
{
    public class Grade
    {
    public string? id { get; set; }

    public string? nomenklatur { get; set; }

    public string? kompetensi { get; set; }
    
    public string? WorkExp { get; set; }

    public string? MinSalary { get; set; }

    public string? MidSalary { get; set; }

    public string? MaxSalary { get; set; }

    public string? MinEdu { get; set; }    
}
}