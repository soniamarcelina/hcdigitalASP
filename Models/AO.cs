using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace hcdigital.Models
{
    [Table("assignmentorder")]
    public class AO
    {
       public int id {get; set;}
       public string? AO_No {get; set;}
       public string? contractor_id {get; set;}
       public string? status {get; set;}
       public int mrf_id {get; set;}
       public int PositionID{get; set;}
       public int id_personnel {get; set;}
       public int personnel_salary {get; set;}
       public long personnel_billing{get; set;}
       public string? personnel_category {get; set;}
       public DateTime? contract_start {get; set;}
       public DateTime? contract_end {get; set;}
       public string? point_of_hire {get; set;}
       public int created_by {get; set;}
       public string? tempKey {get; set;}
       
    }
}