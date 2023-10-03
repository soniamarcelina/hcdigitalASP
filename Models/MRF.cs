using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hcdigital.Models
{
    public class MRF
    {
        public int id_mrf {get; set;}
        public string? mrf_code {get; set;}
        public int yID {get; set;}
        public string? status {get; set;}
        public string? mrf_type {get; set;}
        public int id_position {get; set;}
        public string? workTerm {get; set;}
        public string? ABI_ABO {get; set;}
        public int prev_mrf {get; set;}
        public string? justification {get; set;}
        public DateTime? start_date {get; set;}
        public DateTime? end_date {get; set;}
        public int RequestorID {get; set;}
        public int created_by {get; set;}
        public string? tempKey {get; set;}
        //public Position? Position { get; set; }
    }
}