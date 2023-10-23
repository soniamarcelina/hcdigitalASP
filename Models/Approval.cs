using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hcdigital.Models
{
    public class Approval
    {
        public int id {get; set;}
        public string? type {get; set;}
        public int type_id {get; set;}
        public string? approver_role {get; set;}
        public int approver_id {get; set;}
        public int approver_order {get; set;}
        public int approver_persID {get; set;}
        public string? status {get; set;}
        public string? comments {get; set;}
    }
}