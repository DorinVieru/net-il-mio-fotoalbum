using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Runtime.ConstrainedExecution;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public PhotoController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var photos = User.IsInRole("SuperAdmin") ? PhotoManager.GetAllPhotos() : PhotoManager.GetPhotosByUser(userId);
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
        [Authorize(Roles = "SuperAdmin, Photographer")]
        public IActionResult Create()
        {
            Photo p = new Photo();

            PhotoFormModel model = new PhotoFormModel(p);
            model.CreateCategories();

            return View(model);
        }

        // CREAZIONE POST al click del pulsante CREA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel photoInsert)
        {
            if (!ModelState.IsValid)
            {
                photoInsert.CreateCategories();
                return View("Create", photoInsert); // Ritorna alla view in cui è presente il form
            }

            photoInsert.Photo.AuthorId = _userManager.GetUserId(User);
            photoInsert.SetImageFileFromFormFile();
            PhotoManager.InsertPhoto(photoInsert.Photo, photoInsert.SelectedCategories);

            return RedirectToAction("Index");
        }

        // MODIFICA GET 
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Photographer")]
        public IActionResult Update(int id)
        {
            var photoUpdate = PhotoManager.GetPhotoById(id);
            if (photoUpdate == null)
                return NotFound();

            // Verifico che l'utente sia l'autore della foto o un SuperAdmin
            if (!User.IsInRole("SuperAdmin") && photoUpdate.AuthorId != _userManager.GetUserId(User))
                return Forbid();

            PhotoFormModel model = new PhotoFormModel(photoUpdate);
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

            var originalPhoto = PhotoManager.GetPhotoById(id);
            if (originalPhoto == null)
                return NotFound();

            // Verifico che l'utente sia l'autore della foto o un SuperAdmin
            if (!User.IsInRole("SuperAdmin") && originalPhoto.AuthorId != _userManager.GetUserId(User))
                return Forbid();

            photoUpdate.SetImageFileFromFormFile();
            if (PhotoManager.UpdatePhoto(id, photoUpdate.Photo, photoUpdate.SelectedCategories))
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        // CNACELLAZIONE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Photographer")]
        public IActionResult Delete(int id)
        {
            var photoToDelete = PhotoManager.GetPhotoById(id);
            if (photoToDelete == null)
                return NotFound();

            // Verifico che l'utente sia l'autore della foto o un SuperAdmin
            if (!User.IsInRole("SuperAdmin") && photoToDelete.AuthorId != _userManager.GetUserId(User))
                return Forbid();

            PhotoManager.DeletePhoto(id);
            return RedirectToAction("Index");
        }
    }
}
