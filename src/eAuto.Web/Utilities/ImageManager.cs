using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;

namespace eAuto.Web.Utilities
{
    public sealed class ImageManager : IImageManager
    {
        public List<string> FilesName { get; set; }
        private readonly string _webRootPath;
        private readonly ILogger<ImageManager> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageManager(
            ILogger<ImageManager> logger,
            IWebHostEnvironment hostEnvironment)
        {
            FilesName= new();
            _hostEnvironment = hostEnvironment;
            _webRootPath = _hostEnvironment.WebRootPath;
            _logger = logger;
        }
        public void UploadFiles(object files, string path)
        {
            try
            {
                string upload = string.Concat(_webRootPath, path);
                var formFiles = files as IFormFileCollection;
                if (files == null)
                {
                    throw new ImageCastException();
                }
                else
                {
                    foreach (var file in formFiles!)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(file.FileName);
                        string fullFileName = string.Concat(fileName, extension);
                        FilesName.Add(fullFileName);
                        using (var fileStream = new FileStream(Path.Combine(upload, fullFileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Files upload is failed");
            }
        }
        public void RemoveFile(string contentWay, string nameFile)
        {
            try
            {
                if (nameFile is not null)
                {
                    string _filePath = Path.Combine(string.Concat(_webRootPath, nameFile));
                    if (File.Exists(_filePath))
                    {
                        File.Delete(_filePath);
                    }
                }
            }
            catch (GenericNotFoundException<ImageManager> ex)
            {
                _logger.LogError(ex, "File not found");
            }
        }
    }
}