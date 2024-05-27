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

        // PRENDERE TUTTE LE CATEGORIE DELLE FOTO
        public static List<Category> GetAllCategories()
        {
            using PhotoContext db = new PhotoContext();
            return db.Categories.ToList();
        }
    }
}
