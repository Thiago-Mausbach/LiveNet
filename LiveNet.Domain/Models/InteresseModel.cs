using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveNet.Domain.Models
{
    public class InteresseModel
    {
        public Guid Id { get; set; }

        [StringLength(30)]
        public required string Interesse { get; set; }
    }
}
