using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Application.Responses
{
    public class APIError
    {
        public string Discription { get; set; }
        public APIError(string discription)
        {
            Discription=discription;
        }
    }
}
