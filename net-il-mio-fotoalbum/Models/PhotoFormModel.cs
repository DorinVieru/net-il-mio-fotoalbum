using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; }
        public List<SelectListItem>? Categories { get; set; } // Categorie selezionabili 
        public List<string>? SelectedCategories { get; set; } // Gli ID delle categorie effettivamente selezionate
        public IFormFile? ImageFormFile { get; set; } // Immagine da caricare

        public PhotoFormModel() { }

        public PhotoFormModel(Photo photo)
        {
            Photo = photo;
            SelectedCategories = new List<string>();
            if (Photo.Categories != null)
                foreach (var c in Photo.Categories)
                    SelectedCategories.Add(c.Id.ToString());
        }

        public void CreateCategories()
        {
            this.Categories = new List<SelectListItem>();
            if (this.SelectedCategories == null)
                this.SelectedCategories = new List<string>();

            var categoriesFromDb = PhotoManager.GetAllCategories();
            foreach (var cat in categoriesFromDb)
            {
                bool isSelected = this.SelectedCategories.Contains(cat.Id.ToString());
                this.Categories.Add(new SelectListItem() // lista degli elementi selezionabili
                {
                    Text = cat.Name,
                    Value = cat.Id.ToString(),
                    Selected = isSelected
                });
            }
        }

        // Travasa i dati di ImageFormFile in Photo.ImgFile(da IFormFile a byte[])
        public byte[] SetImageFileFromFormFile()
        {
            if (ImageFormFile == null)
                return null;

            using var stream = new MemoryStream();
            this.ImageFormFile?.CopyTo(stream);
            Photo.ImgFile = stream.ToArray();

            return Photo.ImgFile;
        }
    }
}
