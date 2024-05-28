namespace net_il_mio_fotoalbum.Models
{
    public class ContactFormManager
    {
        public static void InsertMessage(ContactMessage message)
        {
            using PhotoContext db = new PhotoContext();
            
            db.Messages.Add(message);
            db.SaveChanges();
        }
    }
}
