using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hcdigital.Models
{
    public class Competency
    {
        public int id {get; set;}
        public string? name {get; set;}
        public string? skill_group {get; set;}
        public string? sub_skill_group {get; set;}
        public string? definition {get; set;}
        public int level {get; set;}
        public string? indicator {get; set;}
    }
}