 using InstantShop.Application.AuthClass;
using InstantShop.Application.Responses;
using InstantShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InstantShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected APIResponse _response;
        public UserController(IAuthService authService)
        {
            _authService = authService;
            _response = new APIResponse();

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<APIResponse>> Register(Register register)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.DisplayMessage = CommonMessages.RegisterOperationFiled;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return _response;
                }
              var result =  await _authService.Reister(register);

                _response.DisplayMessage = CommonMessages.RegisterOperationSuccess;
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = result;
                _response.IsSuccess = true;

            }
            catch (Exception)
            {
                _response.AddError(CommonMessages.SystemError);
                _response.DisplayMessage = CommonMessages.RegisterOperationFiled;
                _response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<APIResponse>> Login(Login login)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.DisplayMessage = CommonMessages.LoginOperationFiled;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return _response;
                }
                var logg = await _authService.Login(login);
                if (logg is string)
                {
                    _response.DisplayMessage = CommonMessages.LoginOperationFiled;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Result = logg;
                    return _response;
                }


                _response.DisplayMessage = CommonMessages.LoginOperationSuccess;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = logg;
                _response.IsSuccess = true;

            }
            catch (Exception)
            {
                _response.AddError(CommonMessages.SystemError);
                _response.DisplayMessage = CommonMessages.LoginOperationFiled;
                _response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return _response;
        }


    }
}
