using AutoMapper;
using mantenimiento_api.Controllers.RR.VM;
using mantenimiento_api.Controllers.RR;
using mantenimiento_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mantenimiento_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        readonly ILogger _logger;
        readonly IImagesServices _services;
        public ImagesController(ILogger<ImagesController> logger, IImagesServices services) 
        {
            _logger = logger;
            _services = services;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<ApiResponseBase<bool>>> Post(IFormFile file, int idWorkOrder) 
        {
            ApiResponseBase<bool> resp = new ApiResponseBase<bool>();
            resp.Successful();

            try
            {
                List<IFormFile> formFiles = new List<IFormFile>
                {
                    file
                };

                _services.UploadWorkOrderImages(formFiles,idWorkOrder);
                resp.Data = true;
            }
            catch (Exception e)
            {
                resp.Error(e.Message);
                resp.Data = false;
                _logger.LogError(e.Message);
                return resp;
            }

            return resp;
        }

        [HttpDelete]
        [Route("WorkOrderImages/{idWorkOrder}")]
        public async Task<ActionResult<ApiResponseBase<bool>>> WorkOrderImages(int idWorkOrder)
        {
            ApiResponseBase<bool> resp = new ApiResponseBase<bool>();
            resp.Successful();

            try
            {
                _services.DeleteWorkOrderImages(idWorkOrder);
                resp.Data = true;
            }
            catch (Exception e)
            {
                resp.Error(e.Message);
                resp.Data = false;
                _logger.LogError(e.Message);
                return resp;
            }

            return resp;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseBase<bool>>> Delete(int id)
        {
            ApiResponseBase<bool> resp = new ApiResponseBase<bool>();
            resp.Successful();

            try
            {
                _services.DeleteImages(id);
                resp.Data = true;
            }
            catch (Exception e)
            {
                resp.Error(e.Message);
                resp.Data = false;
                _logger.LogError(e.Message);
                return resp;
            }

            return resp;
        }
    }
}
