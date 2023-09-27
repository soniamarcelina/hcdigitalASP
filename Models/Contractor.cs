using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using hcdigital.Data;

namespace hcdigital.Models
{
    public class Contractor
    {
        public string? contractNo {get; set;}
        public string? Name {get; set;}
        public string? Title {get; set;}
        public string? Zona {get; set;}
        public string? Company {get; set;}
        public string? WorkLoc {get; set;}
        public int? Headcount{get; set;}
        public DateTime? start_tender {get; set;}
        public DateTime? end_tender {get; set;}
        public DateTime? start_date {get; set;}
        public DateTime? end_date {get; set;}
        public DateTime? start_date_amd {get; set;}
        public DateTime? end_date_amd {get; set;}
        public int? jml_amandemen{get; set;}
        public long? nilai_kontrak{get; set;}
        public float? service_fee{get; set;}
       

    }
}