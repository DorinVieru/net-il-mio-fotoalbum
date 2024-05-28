using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            var categories = CategoryManager.GetAllCategories();
            return View(categories);
        }

        // PAGINA DETTAGLIO CATEGORIA
        public IActionResult Details(int id)
        {
            var category = CategoryManager.GetCategoryById(id, true);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // CREAZIONE GET
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            CategoryFormModel model = new CategoryFormModel();
            model.Category = new Category();
            model.CreatePhotos();
            return View(model);
        }

        // CREAZIONE POST al click del pulsante CREA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.CreatePhotos();
                return View("Create", data); // Ritorna alla view in cui è presente il form
            }

            CategoryManager.InsertCategory(data.Category, data.SelectedPhotos);
            return RedirectToAction("Index");
        }

        // MODIFICA GET 
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Update(int id)
        {
            var categoryUpdate = CategoryManager.GetCategoryById(id);
            if (categoryUpdate == null)
                return NotFound();

            CategoryFormModel model = new CategoryFormModel(categoryUpdate);
            model.CreatePhotos();
            return View(model);
        }

        // MODIFICA POST al click del punsante MODIFICA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, CategoryFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.CreatePhotos();
                return View("Update", data); // Ritorna alla view in cui è presente il form di modifica
            }

            if (CategoryManager.UpdateCategory(id, data.Category, data.SelectedPhotos))
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        // CNACELLAZIONE CATEGORIA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (CategoryManager.DeleteCategory(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }
    }
}
