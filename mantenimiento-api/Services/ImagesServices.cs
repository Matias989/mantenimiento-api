using mantenimiento_api.Models;
using mantenimiento_api.Services.Interfaces;

namespace mantenimiento_api.Services
{
    public class ImagesServices : IImagesServices
    {
        readonly ILogger _logger;
        readonly MantenimientoApiContext _context;
        readonly IWebHostEnvironment _environment;
        readonly IConfiguration _configuration;
        public ImagesServices(ILogger<ImagesServices> logger, MantenimientoApiContext context, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }

        public void UploadWorkOrderImages(List<IFormFile> files, int idWorkOrder) 
        {
            try
            {
                _logger.LogInformation("Upload Image");

                List<Picture> pictureList = new List<Picture>();
                string extencionPath = _configuration["ResourcesPath:WorkOrdersImages"];


                foreach (var file in files)
                {
                    var picture = new Picture();
                    picture.Title = Path.GetFileNameWithoutExtension(file.FileName);
                    picture.IdWorkOrder = idWorkOrder;
                    picture.Path = _environment.ContentRootPath +
                        extencionPath +
                        picture.Title +
                        "_" +
                        DateTime.UtcNow.ToString("ddMMyyhhmmss") +
                        Path.GetExtension(file.FileName);

                    using (var stream = File.Create(picture.Path)) file.CopyTo(stream);

                    pictureList.Add(picture);
                }

                _context.Pictures.AddRange(pictureList);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public void DeleteWorkOrderImages(int idWorkOrder)
        {
            try
            {
                _logger.LogInformation("Delete Work Order Images");

                string extencionPath = _configuration["ResourcesPath:WorkOrdersImages"];

                var pictures = _context.Pictures.Where(x => x.IdWorkOrder == idWorkOrder);

                foreach (var picture in pictures)
                {
                    File.Delete(picture.Path);
                }

                _context.Pictures.RemoveRange(pictures);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public void DeleteImages(int idPicture)
        {
            try
            {
                _logger.LogInformation("Delete Images");

                string extencionPath = _configuration["ResourcesPath:WorkOrdersImages"];

                var picture = _context.Pictures.First(x => x.Id == idPicture);

                File.Delete(picture.Path);

                _context.Pictures.Remove(picture);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
