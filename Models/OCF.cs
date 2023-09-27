using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hcdigital.Models
{
    public class OCF
    {
        public int id {get; set;}
        public string? Code {get; set;}
        public string? subject {get; set;}
        public string? tempKey {get; set;}
        public int yID {get; set;}
        public int RequestorID {get; set;}
        public int DirectPos {get; set;}
        public string? PosTitle {get; set;}
        public int Headcount {get; set;}
        public string? Grade {get; set;}
        public string? WorkLoc {get; set;}
        public string? WorkSch {get; set;}
        public string? WorkCity{get; set;}
        public string? costType {get; set;}
        public string? costCenter {get; set;}
        public string? Justification {get; set;}
        public string? Responsibility {get; set;}
        public string? Qualification{get; set;}
        public string? Competency {get; set;}
        public string? Education {get; set;}
        public string? Experience {get; set;}
        public string? Skill {get; set;}
        public int CreatorID {get; set;}
        public string? CreatorPos {get; set;}
        public string? Attachment {get; set;}

    }
}