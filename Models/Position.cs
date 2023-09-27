using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hcdigital.Models
{
    public class Position
    {
        public int? Id_Position {get; set;}
        public int? ID_AO {get; set;}
        public string? PosTitle {get; set;}
        public string? DirectPos_ID {get; set;}
        public string? Direktorat{get; set;}
        public string? Division{get; set;}
        public string? Sub_division{get; set;}
        public string? Department{get; set;}
        public string? Section{get; set;}
        public string? Company_ID{get; set;}
        public string? PersArea_ID{get; set;}
        public string? PersSubArea_ID{get; set;}
        public string? CostType{get; set;}
        public string? CostCenter{get; set;}
        public string? Work_Schedule{get; set;}
        public string? Grade{get; set;}
        public string? Responsibility{get; set;}
        public string? Qualification{get; set;}
        public string? Competency{get; set;}
        public string? Education{get; set;}
        public string? Experience{get; set;}
        public string? Skill{get; set;}
        public string? Status{get; set;}
        public string? Ocf_id{get; set;}

         public DirectPos? masteremployee { get; set; }
      
    }
}