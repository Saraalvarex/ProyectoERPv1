using System.IO;

namespace ProyectoERP.Helpers
{
    public class HelperUploadFiles
    {
        private readonly HelperPathProvider helperPath;
        private IWebHostEnvironment hostEnvironment;

        public HelperUploadFiles(HelperPathProvider pathProvider, IWebHostEnvironment hostEnvironment)
        {
            this.helperPath = pathProvider;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string fileName, string host, Folders folder)
        {
            string type = file.FileName.Split('.')[1];
            string carpeta = this.helperPath.MapPath(fileName, folder);
            string finalFileName = fileName + "." + type;
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, finalFileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return Path.Combine(host, carpeta, finalFileName);
            }
        }
    }
}
