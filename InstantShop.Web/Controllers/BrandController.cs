using InstantShop.Application.DTO_s.Brand;
using InstantShop.Application.Responses;
using InstantShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InstantShop.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        protected readonly IBrandService _brandService;
        private APIResponse _response;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
            _response = new APIResponse();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Create([FromBody]CreateBrandDto createBrandDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    _response.AddError(ModelState.ToString());
                    _response.DisplayMessage = CommonMessages.CreateOperationFiled;
                    return Ok(_response);
                }
                var brand = await _brandService.CreateAsync(createBrandDto);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.Result = brand;
                _response.DisplayMessage = CommonMessages.CreateOperationSuccess;
               
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessages.SystemError);
                _response.DisplayMessage = CommonMessages.CreateOperationFiled;
                throw;
            }
            return Ok(_response);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Get()
        {
            try
            {
               
                var brand = await _brandService.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brand;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessages.SystemError);
               
                throw;
            }
            return Ok(_response);
        }

        [HttpGet]
        [Route("Details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessages.RecoredNotFound;
                    return Ok(_response);
                }

                var brand = await _brandService.GetByIdAsync(id);
                if (brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessages.RecoredNotFound;
                    return Ok(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brand;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessages.SystemError);

                throw;
            }
            return Ok(_response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateBrandDto updateBrandDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.AddError(ModelState.ToString());
                    _response.DisplayMessage = CommonMessages.UpdateOperationFiled;
                    return Ok(_response);
                }

                var brand = await _brandService.GetByIdAsync(updateBrandDto.Id);
                if (brand==null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessages.UpdateOperationFiled;
                    _response.DisplayMessage = CommonMessages.RecoredNotFound;
                    return Ok(_response);
                }


                await _brandService.UpdateAsync(updateBrandDto);
                
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessages.UpdateOperationSuccess;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessages.SystemError);
                _response.DisplayMessage = CommonMessages.UpdateOperationFiled;
                throw;
            }
            return Ok(_response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id==0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommonMessages.DeleteOperationFiled;
                    return Ok(_response);
                }

                var brand = await _brandService.GetByIdAsync(id);
                if (brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommonMessages.DeleteOperationFiled;
                    _response.DisplayMessage = CommonMessages.RecoredNotFound;
                    return Ok(_response);
                }


                await _brandService.DeleteAsync(id);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessages.DeleteOperationSuccess;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessages.SystemError);
                _response.DisplayMessage = CommonMessages.DeleteOperationFiled;
                throw;
            }
            return Ok(_response);
        }

    }
}
