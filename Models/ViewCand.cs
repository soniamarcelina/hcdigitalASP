using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace hcdigital.Models
{
    public class ViewCand
    {
         public MRF? MRF { get; set; }
        public Position? Position { get; set; }
        public DirectPos? DirectPos { get; set; }
    }
}