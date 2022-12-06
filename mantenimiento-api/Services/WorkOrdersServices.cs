using mantenimiento_api.Controllers;
using mantenimiento_api.Models;
using mantenimiento_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace mantenimiento_api.Services
{
    public class WorkOrdersServices : IWorkOrdersServices
    {
        readonly ILogger _logger;
        readonly MantenimientoApiContext _context;
        public WorkOrdersServices(ILogger<WorkOrdersServices> logger, MantenimientoApiContext context)
        {
            _logger = logger;
            _context = context;
        }
        public bool DeleteWorkOrder(int id)
        {
            bool result = false;
            try
            {
                if (id <= 0) 
                {
                    _logger.LogError("Valor invalido de id");
                    return result;
                }
                var workOrders = _context.WorkOrders.FirstOrDefault(x=> x.Id == id);
                if (workOrders == null)
                {
                    _logger.LogError("Orden de trabajo no encontrada");
                    return result;
                }

                _context.WorkOrders.Remove(workOrders);
                _context.SaveChanges();
                result = true;
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return result;
            }
        }

        public WorkOrder? GetWorkOrder(int id)
        {
            try
            {
                return _context.WorkOrders
                    .Include(w => w.IdUserAsignedNavigation)
                    .Include(w => w.IdUserCreatorNavigation)
                    .FirstOrDefault(x=> x.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public List<WorkOrder> GetWorkOrders()
        {
            try
            {
                return _context.WorkOrders
                    .Include(w => w.IdUserAsignedNavigation)
                    .Include(w => w.IdUserCreatorNavigation)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<WorkOrder>();
            }
        }

        public int InsertWorkOrder(WorkOrder workOrder)
        {
            int idInserted = 0;
            try
            {
                if (workOrder is null)
                {
                    _logger.LogError("Valor invalido ingresado");
                    return idInserted;
                }

                _context.WorkOrders.Add(workOrder);
                if (_context.SaveChanges() > idInserted) idInserted = workOrder.Id;

                return idInserted;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return idInserted;
            }
        }

        public bool UpdateWorkOrder(WorkOrder workOrder)
        {
            bool result = false;
            try
            {
                if (workOrder is null)
                {
                    _logger.LogError("Valor invalido ingresado");
                    return result;
                }

                _context.Update(workOrder);
                if (_context.SaveChanges() > 0) result = true;
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return result;
            }
        }
    }
}
