namespace mantenimiento_api.Services.Interfaces
{
    public interface IImagesServices
    {
        void UploadWorkOrderImages(List<IFormFile> files, int idWorkOrder);
        void DeleteImages(int idPicture);
        void DeleteWorkOrderImages(int idWorkOrder);
    }
}
