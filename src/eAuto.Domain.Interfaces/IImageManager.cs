namespace eAuto.Domain.Interfaces
{
    public interface IImageManager
    {
        public List<string> FilesName { get; }
        public void UploadFiles(Object files, string path);
        public void RemoveFile(string contentWay, string nameFile);
    }
}