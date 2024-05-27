using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize]
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

        // CREAZIONE GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            Photo p = new Photo();

            PhotoFormModel model = new PhotoFormModel(p);
            model.CreateCategories();

            return View(model);
        }

        // CREAZIONE POST al click del pulsante CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel photoInsert)
        {
            if (!ModelState.IsValid)
            {
                photoInsert.CreateCategories();
                return View("Create", photoInsert); // Ritorna alla view in cui è presente il form
            }

            photoInsert.SetImageFileFromFormFile();
            PhotoManager.InsertPhoto(photoInsert.Photo, photoInsert.SelectedCategories);

            return RedirectToAction("Index");
        }

        // MODIFICA GET 
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var pizzaModificata = PhotoManager.GetPhotoById(id);
            if (pizzaModificata == null)
                return NotFound();

            PhotoFormModel model = new PhotoFormModel(pizzaModificata);
            model.CreateCategories();

            return View(model);
        }

        // MODIFICA POST al click del punsante MODIFICA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PhotoFormModel photoUpdate)
        {
            if (!ModelState.IsValid)
            {
                photoUpdate.CreateCategories();
                return View("Update", photoUpdate); // Ritorna alla view in cui è presente il form di modifica
            }

            photoUpdate.SetImageFileFromFormFile();
            if (PhotoManager.UpdatePhoto(id, photoUpdate.Photo, photoUpdate.SelectedCategories))
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        // CNACELLAZIONE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            PhotoManager.DeletePhoto(id);
            return RedirectToAction("Index");
        }
    }
}
