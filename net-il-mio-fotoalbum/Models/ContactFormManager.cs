
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Models
{
    public class ContactFormManager
    {
        // REGISTRAZIONE DI UN NUOVO MESSAGGIO INVIATO DALL'UTENTE NEL DATABASE
        public static void InsertMessage(ContactMessage message)
        {
            using PhotoContext db = new PhotoContext();
            
            db.Messages.Add(message);
            db.SaveChanges();
        }

        // OTTENERE LA LISTA DI TUTTE I MESSAGGI
        public static List<ContactMessage> GetAllMessages()
        {
            using PhotoContext db = new PhotoContext();

            return db.Messages.ToList();
        }

        // RECUPERARE UN MESSAGGIO TRAMITE IL SUO ID
        public static ContactMessage GetMessageById(int id)
        {
            try
            {
                using PhotoContext db = new PhotoContext();

                return db.Messages.FirstOrDefault(c => c.Id == id);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore: messaggio non trovato. -> {ex.Message}");
                return null;
            }
        }

        // CANCELLARE UNA CATEGORIA
        public static bool DeleteMessage(int id)
        {
            try
            {
                var messageDelete = GetMessageById(id);
                if (messageDelete == null)
                    return false;

                using PhotoContext db = new PhotoContext();
                db.Remove(messageDelete);
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
