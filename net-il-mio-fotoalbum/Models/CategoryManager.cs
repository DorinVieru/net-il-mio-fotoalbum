using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace net_il_mio_fotoalbum.Models
{
    public class CategoryManager
    {
        // AGGIUNTA DI UNA CATEGORIA
        public static void InsertCategory(Category Category)
        {
            using PhotoContext db = new PhotoContext();

            db.Categories.Add(Category);
            db.SaveChanges();

        }

        // RECUPERARE UNA CATEGORIA TRAMITE IL SUO ID
        public static Category GetCategoryById(int id, bool includeReferences = true)
        {
            try
            {
                using PhotoContext db = new PhotoContext();

                if (includeReferences)
                    return db.Categories.Where(c => c.Id == id).Include(p => p.photos).FirstOrDefault();
                return db.Categories.FirstOrDefault(p => p.Id == id);

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
            var category = db.Categories.Where(c => c.Id == id).Include(p => p.photos).FirstOrDefault();

            if (category == null)
                return false;

            category.Name = dataCategory.Name;

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
    } 
}
