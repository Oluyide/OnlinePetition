using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLocalGovt.Models
{
    public class PetitionSignatureModel
    {
     
        public int Selected { get; set; }
        public int PetitionId { get; set; }
        public int SignId { get; set; }

        public string SignFirstName { get; set; }
        public string SignLastName { get; set; }
        public string SignEmail { get; set; }
        public string SignAdd { get; set; }
        public string SignCountry { get; set; }
        public string SignState { get; set; }
        public string SignCity { get; set; }
        public string SignWord { get; set; }

    }
}