using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            var messages = ContactFormManager.GetAllMessages();
            return View(messages);
        }

        public IActionResult Details(int id)
        {
            var message = ContactFormManager.GetMessageById(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            ContactFormManager.DeleteMessage(id);
            return RedirectToAction("Index");
        }
    }
}
