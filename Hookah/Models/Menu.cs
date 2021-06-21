using Hookah.Abstracts;

namespace Hookah.Models
{
    public class Menu:BaseEntity
    {
        public string ImageTitle { get; set; }
        public string ImageLP { get; set; }
        public string ImagePL { get; set; }
        public string ImageMB { get; set; }
        public string FlavorsTitle { get; set; }
        public string FlavorsText { get; set; }
        public string FruitHeadTitle { get; set; }
        public string FruitHeadText { get; set; }
    }
}
