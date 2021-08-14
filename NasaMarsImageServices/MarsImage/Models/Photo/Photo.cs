using System.Collections.Generic;

namespace NasaMarsImageServices.Models
{
    public class Photo
    {
        public int id { get; set; }
        public string sol { get; set; }
        public Camera camera { get; set; }
        public string img_src { get; set; }
        public string earth_date { get; set; }
        public Rover rover { get; set; }
    }

    public class PhotoList
    {
        public List<Photo> photos { get; set; }
    }
}
