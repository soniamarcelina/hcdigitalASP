using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace hcdigital.Models
{
    [Table("masteremployee")]
    public class DirectPos
    {
        public string? ID_Position {get; set;}
        public string? Position {get; set;}
        public string? Person_Id {get; set;}
        public string? Pers_No {get; set;}
        public string? Name {get; set;}
        public string? ID_Org_Unit{get; set;}
        public string? Divisi {get; set;}
        public string? Sub_Division {get; set;}
        public string? Departemen {get; set;}
        public string? Section {get; set;}
        public string? KBO {get; set;}
        public string? coCode {get; set;}
        public string? Company_Name {get; set;}
        public string? Org_Unit_Name {get; set;}
        public string? Cost_Center_Posisi {get; set;}
        public string? Cost_Center_Description {get; set;}
        public string? ID_Position_Atasan {get; set;}
        public string? Position_Atasan {get; set;}
        public int ID_Pos_Chief {get; set;}
        public string? Chief_Pos {get; set;}
        public string? GL {get; set;}
        public string? photo {get; set;}

    }
}