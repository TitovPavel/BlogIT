using System.IO;
using BlogIT.DB.BL;
using BlogIT.DB.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace BlogIT.MVC.Controllers
{
    public class FileController : Controller
    {

        private readonly IPhotoService _photoService;
        private readonly IWebHostEnvironment _environment;

        public FileController(IPhotoService photoService,
            IWebHostEnvironment environment)
        {
            _photoService = photoService;
            _environment = environment;
        }

        public ActionResult GetImage(int fileID)
        {
            FileModel file = _photoService.GetFileByID(fileID);

            if (file != null)
            {
                string path = Path.Combine(_environment.WebRootPath, file.Path);
                if (System.IO.File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.Open);
                    string file_type = "image/jpg";
                    return File(fs, file_type, file.Name);
                }
            }

            return null;
        }
    }
}