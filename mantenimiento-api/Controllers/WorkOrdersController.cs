using AutoMapper;
using mantenimiento_api.Controllers.RR;
using mantenimiento_api.Controllers.RR.VM;
using mantenimiento_api.Models;
using mantenimiento_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mantenimiento_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkOrdersController : Controller
    {
        readonly ILogger _logger;
        readonly IWorkOrdersServices _services;
        readonly IMapper _mapper;
        public WorkOrdersController(ILogger<WorkOrdersController> logger, IWorkOrdersServices services, IMapper mapper)
        {
            _logger = logger;
            _services = services;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            ApiResponseBase<List<WorkOrderVM>> resp = new ApiResponseBase<List<WorkOrderVM>>();
            resp.Successful();
            try
            {
                var respService = _services.GetWorkOrders();
                resp.Data = _mapper.Map<List<WorkOrderVM>>(respService);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }

            return Ok(resp);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("find")]
        public IActionResult Get([FromQuery]int id)
        {
            ApiResponseBase<WorkOrderVM> resp = new ApiResponseBase<WorkOrderVM>();
            resp.Successful();
            try
            {
                if (id <= 0)
                {
                    resp.Error("id enviado es incorrecto");
                    return BadRequest(resp);
                }

                resp.Data = _mapper.Map<WorkOrder,WorkOrderVM> (_services.GetWorkOrder(id));

                if (resp.Data is null)
                {
                    var msj = "no se encontro orden de trabajo con id: " + id;
                    _logger.LogError(msj);
                    return BadRequest(msj);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }

            return Ok(resp);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("publish")]
        public IActionResult Post([FromBody] WorkOrderVM workOrderVM)
        {
            ApiResponseBase<WorkOrderVM> resp = new ApiResponseBase<WorkOrderVM>();
            resp.Successful();
            try
            {
                if (workOrderVM.UserCreator is null || string.IsNullOrEmpty(workOrderVM.Observation))
                {
                    string msjError = "Valores obligatorios ingresados incorrectos";
                    resp.Error(msjError);
                    _logger.LogError(msjError);
                    return BadRequest(resp);
                }
                var mappedWO = _mapper.Map<WorkOrderVM, WorkOrder>(workOrderVM);
                var idWorkOrder = _services.InsertWorkOrder(mappedWO);

                if (idWorkOrder < 0) 
                {
                    string msjError = "Ocurrio un problema insertando el registro";
                    _logger.LogError(msjError);
                    return Problem(msjError);
                }

                workOrderVM.Id = idWorkOrder;
                resp.Data = workOrderVM;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }

            return Ok(resp);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("update")]
        public IActionResult Put([FromBody] WorkOrderVM workOrderVM)
        {
            ApiResponseBase<WorkOrderVM> resp = new ApiResponseBase<WorkOrderVM>();
            resp.Successful();
            try
            {
                if (workOrderVM.UserCreator is null || string.IsNullOrEmpty(workOrderVM.Observation))
                {
                    string msjError = "Valores obligatorios ingresados incorrectos";
                    resp.Error(msjError);
                    _logger.LogError(msjError);
                    return BadRequest(resp);
                }
                var mappedWO = _mapper.Map<WorkOrderVM, WorkOrder>(workOrderVM);
                var idWorkOrder = _services.UpdateWorkOrder(mappedWO);

                if (!idWorkOrder)
                {
                    string msjError = "Ocurrio un problema insertando el registro";
                    _logger.LogError(msjError);
                    return Problem(msjError);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }

            return Ok(resp);
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("remove")]
        public IActionResult Delete([FromQuery] int idWorkOrder)
        {
            ApiResponseBase<WorkOrderVM> resp = new ApiResponseBase<WorkOrderVM>();
            resp.Successful();
            try
            {
                if (idWorkOrder < 0)
                {
                    string msjError = "Valor ingresado incorrectos";
                    resp.Error(msjError);
                    _logger.LogError(msjError);
                    return BadRequest(resp);
                }

                var respService = _services.DeleteWorkOrder(idWorkOrder);

                if (!respService)
                {
                    string msjError = "Ocurrio un problema eliminando el registro";
                    _logger.LogError(msjError);
                    return Problem(msjError);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }

            return Ok(resp);
        }

    }
}
