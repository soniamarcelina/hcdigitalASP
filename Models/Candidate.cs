using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hcdigital.Models
{
    public class Candidate
    {
        public int id {get; set;}
        public int personnel_id {get; set;}
        public int mrf_id {get; set;}
        public string? status {get; set;}
        public DateTime? interview_date {get; set;}
        public string? interview_loc {get; set;}
        public string? comp_score {get; set;}
        public string? akhlak_score {get; set;}
        public string? skill_core {get; set;}
        public int interview_score {get; set;}
        public int current_salary {get; set;}
        public int expected_salary {get; set;}
        public string? advantages {get; set;}
        public string? disadvantages {get; set;}
        public int offer_salary {get; set;}
        public int final_salary{get; set;}
        public string? interview_decision {get; set;}
        public string? interviewer {get; set;}


    }
}