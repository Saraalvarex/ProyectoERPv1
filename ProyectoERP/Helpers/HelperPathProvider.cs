namespace ProyectoERP.Helpers
{
    public enum Folders { FotosAlumnos = 0, FotosUsuarios = 1, Facturas = 2};
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        public HelperPathProvider(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }
        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.FotosAlumnos)
            {
                carpeta = "archivos/fotosalumnos";
            }
            else if (folder == Folders.FotosUsuarios)
            {
                carpeta = "archivos/fotosusuarios";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "archivos/facturas";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }
    }
}
