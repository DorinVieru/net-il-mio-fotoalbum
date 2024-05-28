using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Models
{
    public class CategoryFormModel
    {
        public Category Category { get; set; }
        public List<SelectListItem>? Photos { get; set; }
        public List<string>? SelectedPhotos { get; set; }

        public CategoryFormModel() { }
        public CategoryFormModel(Category c)
        {
            this.Category = c;
        }

        public void CreatePhotos()
        {
            this.Photos = new List<SelectListItem>();
            if (this.SelectedPhotos == null)
                this.SelectedPhotos = new List<string>();
            var photosFromDb = CategoryManager.GetAllPhotos();
            foreach (var photo in photosFromDb)
            {
                bool isSelected = this.SelectedPhotos.Contains(photo.Id.ToString());
                this.Photos.Add(new SelectListItem()
                {
                    Text = photo.Title,
                    Value = photo.Id.ToString(),
                    Selected = isSelected
                });

            }
        }
    }
}
