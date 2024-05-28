using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace net_il_mio_fotoalbum.Models
{
    public class CategoryManager
    {
        // AGGIUNTA DI UNA CATEGORIA
        public static void InsertCategory(Category category, List<string> selectedPhotos = null)
        {
            using PhotoContext db = new PhotoContext();
            
            if (selectedPhotos != null)
            {
                category.photos = new List<Photo>();
                foreach (var photoId in selectedPhotos)
                {
                    int id = int.Parse(photoId);
                    var photo = db.Photos.FirstOrDefault(c => c.Id == id);
                    category.photos.Add(photo);
                }
            }

            db.Categories.Add(category);
            db.SaveChanges();

        }

        // OTTENERE LA LISTA DI TUTTE LE CATEGORIE
        public static List<Category> GetAllCategories()
        {
            using PhotoContext db = new PhotoContext();

            return db.Categories.Include(c => c.photos).ToList();
        }

        // RECUPERARE UNA CATEGORIA TRAMITE IL SUO ID
        public static Category GetCategoryById(int id, bool includeReferences = true)
        {
            try
            {
                using PhotoContext db = new PhotoContext();

                if (includeReferences)
                    return db.Categories.Where(c => c.Id == id).Include(p => p.photos).FirstOrDefault();
                
                return db.Categories.FirstOrDefault(c => c.Id == id);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: categoria non trovata. -> {ex.Message}");
                return null;
            }
        }

        // MODIFICARE UNA CATEGORIA
        public static bool UpdateCategory(int id, Category dataCategory, List<string> photos)
        {
            using PhotoContext db = new PhotoContext();
            var categoryToEdit = db.Categories.Where(c => c.Id == id).Include(p => p.photos).FirstOrDefault();

            if (categoryToEdit == null)
                return false;

            categoryToEdit.Name = dataCategory.Name;

            categoryToEdit.photos.Clear();
            if (photos != null)
            {
                foreach (var photo in photos)
                {
                    int idPhoto = int.Parse(photo);
                    var photoFromDb = db.Photos.FirstOrDefault(p => p.Id == idPhoto);
                    if (photoFromDb != null)
                        categoryToEdit.photos.Add(photoFromDb);
                }
            }

            db.SaveChanges();

            return true;
        }

        // CANCELLARE UNA CATEGORIA
        public static bool DeleteCategory(int id)
        {
            try
            {
                var categoryDelete = GetCategoryById(id);
                if (categoryDelete == null)
                    return false;

                using PhotoContext db = new PhotoContext();
                db.Remove(categoryDelete);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        // RECUPERARE LISTA DI TUTTE LE FOTO
        public static List<Photo> GetAllPhotos()
        {
            using PhotoContext db = new PhotoContext();
            return db.Photos.Include(p => p.Categories).ToList();
        }
    } 
}
