using Microsoft.AspNetCore.Mvc;
using ProyectoERP.Helpers;
using static NuGet.Packaging.PackagingConstants;

namespace ProyectoERP.Controllers
{
    public class MailsController : Controller
    {
        private HelperMail helperMail;

        public MailsController(HelperMail helperMail)
        {
            this.helperMail = helperMail;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string para, string asunto, string mensaje, List<IFormFile> files)
        {
            if (files.Count != 0)
            {
                //if (files.Count > 1)
                //{
                //    List<string> paths = await this.helperUploadFiles.UploadFileAsync(files, Folders.Temporal);
                //    await this.helperMail.SendMailAsync(para, asunto, mensaje, paths);
                //}
                //else
                //{
                //    string path = await this.helperUploadFiles.UploadFileAsync(files[0], Folders.Temporal);
                //    await this.helperMail.SendMailAsync(para, asunto, mensaje, path);
                //}
            }
            else
            {
                await this.helperMail.SendMailAsync(para, asunto, mensaje);
            }
            ViewBag.MENSAJE = "Email enviado correctamente";
            return View();
        }
    }
}
