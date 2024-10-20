using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Application.Responses
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode {  get; set; }
        public bool IsSuccess { get; set; } = false;
        public object Result {  get; set; }
        public string DisplayMessage { get; set; } = "";

        public List<APIError> Errors { get; set; } = new();

        public void AddError(string errormessage)
        {
            APIError error = new APIError(discription:errormessage);
            Errors.Add(error);
        }
    }
}
