using mantenimiento_api.Models;

namespace mantenimiento_api.Services.Interfaces
{
    public interface IWorkOrdersServices
    {
        IEnumerable<WorkOrder> GetWorkOrders();
        WorkOrder? GetWorkOrder(int id);
        int InsertWorkOrder(WorkOrder workOrder);
        bool DeleteWorkOrder(int id);
        bool UpdateWorkOrder(WorkOrder workOrder);
    }
}
