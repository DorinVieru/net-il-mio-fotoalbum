using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotoWebApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPhotos(string? name)
        {
            if(name == null) {
                return Ok(PhotoManager.GetAllPhotos());
            }

            return Ok(PhotoManager.GetAllPhotosFilter(name));
        }

        [HttpGet("{id}")]
        public IActionResult GetPhotoById(int id)
        {
            var photo = PhotoManager.GetPhotoById(id);
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }

        [HttpPost]
        public IActionResult CreatePhoto(Photo photo)
        {
            PhotoManager.InsertPhoto(photo);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePhoto(int id, [FromBody] Photo photo)
        {
            var oldPhoto = PhotoManager.GetPhotoById(id);
            if (oldPhoto == null)
                return NotFound("Errore devastante del sistema! Riprova, sarai più fortunato.");
            
            PhotoManager.UpdatePhoto(id, photo, null);
            return Ok(oldPhoto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePhoto(int id)
        {
            bool found = PhotoManager.DeletePhoto(id);
            if (found)
                return Ok("Foto selezionata cancellata con successo.");
            
            return NotFound();
        }
    }
}
