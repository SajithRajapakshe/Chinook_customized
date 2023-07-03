using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChinookDataAccessLayer.Models
{
    public partial class AlbumData
    {
        public AlbumData()
        {
            Tracks = new HashSet<TrackData>();
        }

        [Key]
        public long AlbumId { get; set; }
        public string Title { get; set; } = null!;
        public long ArtistId { get; set; }

        public virtual ArtistData Artist { get; set; } = null!;
        public virtual ICollection<TrackData> Tracks { get; set; }
    }
}
