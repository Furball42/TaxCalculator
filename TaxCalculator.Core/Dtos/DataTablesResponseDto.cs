using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Core.Dtos
{
    public class DataTablesResponseDto
    {
        public int Draw { get; set; }
        public long RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public object[] Data { get; set; }
        public string Error { get; set; }
    }
}
