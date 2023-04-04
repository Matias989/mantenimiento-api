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
        public async Task<ActionResult<ApiResponseBase<IEnumerable<WorkOrderVM>>>> Get()
        {
            ApiResponseBase<IEnumerable<WorkOrderVM>> resp = new ApiResponseBase<IEnumerable<WorkOrderVM>>();
            resp.Successful();
            try
            {
                var respService = _services.GetWorkOrders();
                resp.Data = _mapper.Map<IEnumerable<WorkOrderVM>>(respService);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }

            return Ok(resp);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ApiResponseBase<WorkOrderVM>>> Get([FromQuery]int id)
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

                var data = _services.GetWorkOrder(id);

                if (data is null)
                {
                    var msj = "no se encontro orden de trabajo con id: " + id;
                    _logger.LogError(msj);
                    resp.Error(msj);
                    return BadRequest(resp);
                }
                resp.Data = _mapper.Map<WorkOrder, WorkOrderVM>(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }

            return Ok(resp);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseBase<int>>> Post([FromBody] WorkOrderVM workOrderVM)
        {
            ApiResponseBase<int> resp = new ApiResponseBase<int>();
            resp.Successful();
            try
            {
                if (workOrderVM.IdUserCreator <= 0 || string.IsNullOrEmpty(workOrderVM.Observation))
                {
                    string msjError = "Valores obligatorios ingresados incorrectos";
                    resp.Error(msjError);
                    _logger.LogError(msjError);
                    return BadRequest(resp);
                }

                var mappedWO = _mapper.Map<WorkOrderVM, WorkOrder>(workOrderVM);
                var idWorkOrder = _services.InsertWorkOrder(mappedWO);

                if (idWorkOrder <= 0) 
                {
                    string msjError = "Ocurrio un problema insertando el registro";
                    _logger.LogError(msjError);
                    return Problem(msjError);
                }
                resp.Data = idWorkOrder;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }

            return Ok(resp);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponseBase<string>>> Put([FromBody] WorkOrderVM workOrderVM)
        {
            ApiResponseBase<string> resp = new ApiResponseBase<string>();
            resp.Successful();
            resp.Data = string.Empty;
            try
            {
                if (workOrderVM.IdUserCreator > 0 || string.IsNullOrEmpty(workOrderVM.Observation))
                {
                    string msjError = "Valores obligatorios ingresados incorrectos";
                    resp.Error(msjError);
                    _logger.LogError(msjError);
                    return BadRequest(resp);
                }
                var mappedWO = _mapper.Map<WorkOrderVM, WorkOrder>(workOrderVM);
                var WorkOrder = _services.UpdateWorkOrder(mappedWO);

                if (!WorkOrder)
                {
                    string msjError = "Ocurrio un problema insertando el registro";
                    _logger.LogError(msjError);
                    resp.Error(msjError);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseBase<string>>> Delete([FromQuery] int idWorkOrder)
        {
            ApiResponseBase<string> resp = new ApiResponseBase<string>();
            resp.Successful();
            resp.Data = string.Empty;
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
                    resp.Error(msjError);
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
