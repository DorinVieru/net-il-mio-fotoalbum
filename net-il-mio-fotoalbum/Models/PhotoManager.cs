namespace net_il_mio_fotoalbum.Models
{
    public class PhotoManager
    {
        // PRENDERE TUTTE LE CATEGORIE DELLE FOTO
        public static List<Category> GetAllCategories()
        {
            using PhotoContext db = new PhotoContext();
            return db.Categories.ToList();
        }
    }
}
