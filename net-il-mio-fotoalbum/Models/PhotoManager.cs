using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoManager
    {
        // AGGIUNTA DI UNA FOTO
        public static void InsertPhoto(Photo Photo, List<string> SelectedCategories = null)
        {
            using PhotoContext db = new PhotoContext();

            if (SelectedCategories != null)
            {
                Photo.Categories = new List<Category>();
                // Trasformo gli ID scelti nel form in categorie da aggiungere alle foto
                foreach (var catID in SelectedCategories)
                {
                    int id = int.Parse(catID);
                    var cat = db.Categories.FirstOrDefault(i => i.Id == id);
                    Photo.Categories.Add(cat);
                }
            }

            db.Photos.Add(Photo);
            db.SaveChanges();

        }

        // RECUPERARE UNA FOTO TRAMITE IL SUO ID
        public static Photo GetPhotoById(int id, bool includeReferences = true)
        {
            try
            {
                using PhotoContext db = new PhotoContext();

                if (includeReferences)
                    return db.Photos.Where(p => p.Id == id).Include(p => p.Categories).FirstOrDefault();
                return db.Photos.FirstOrDefault(p => p.Id == id);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: foto non trovata. -> {ex.Message}");
                return null;
            }
        }

        // OTTENERE LA LISTA DI TUTTE LE FOTO
        public static List<Photo> GetAllPhotos()
        {
            using PhotoContext db = new PhotoContext();

            return db.Photos.Include(p => p.Categories)
                          .ThenInclude(pc => pc.photos).ToList();
        }

        // MODIFICARE UNA FOTO
        public static bool UpdatePhoto(int id, Photo dataPhoto, List<string> categories)
        {
            using PhotoContext db = new PhotoContext();
            var photoEdit = db.Photos.Where(p => p.Id == id).Include(c => c.Categories).FirstOrDefault();

            if (photoEdit == null)
                return false;

            photoEdit.Title = dataPhoto.Title;
            photoEdit.Description = dataPhoto.Description;
            if(dataPhoto.ImgFile != null)
                 photoEdit.ImgFile = dataPhoto.ImgFile;

            photoEdit.Visible = dataPhoto.Visible;

            photoEdit.Categories.Clear(); // Prima svuoto così da salvare solo le informazioni che l'utente ha scelto
            if (categories != null)
            {
                foreach (var cat in categories)
                {
                    int categoryID = int.Parse(cat);
                    var categoryDb = db.Categories.FirstOrDefault(x => x.Id == categoryID);
                    photoEdit.Categories.Add(categoryDb);
                }
            }

            db.SaveChanges();

            return true;
        }

        // CANCELLARE UNA FOTO
        public static bool DeletePhoto(int id)
        {
            try
            {
                var photoDelete = GetPhotoById(id);
                if (photoDelete == null)
                    return false;

                using PhotoContext db = new PhotoContext();
                db.Remove(photoDelete);
                db.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        // PRENDERE TUTTE LE CATEGORIE DELLE FOTO
        public static List<Category> GetAllCategories()
        {
            using PhotoContext db = new PhotoContext();
            return db.Categories.ToList();
        }
    }
}
