using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChinookDataAccessLayer.Models
{
    public partial class ArtistData
    {
        public ArtistData()
        {
            Albums = new HashSet<AlbumData>();
        }

        [Key]
        public long ArtistId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<AlbumData> Albums { get; set; }
    }
}
