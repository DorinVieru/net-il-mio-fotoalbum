using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            var photos = PhotoManager.GetAllPhotos();
            return View(photos);
        }

        // PAGINA DETTAGLIO FOTO
        public IActionResult Details(int id)
        {
            var photo = PhotoManager.GetPhotoById(id, true);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }
    }
}
