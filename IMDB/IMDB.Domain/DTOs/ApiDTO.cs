using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain.DTOs
{
    public class ApiDTO
    {
        public class ApiResponse
        {
            public int status_code { get; set; }
            public string status_message { get; set; }
        }
    }
}
