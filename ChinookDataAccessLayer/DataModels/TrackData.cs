using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChinookDataAccessLayer.Models
{
    public partial class TrackData
    {
        public TrackData()
        {
            InvoiceLines = new HashSet<InvoiceLineData>();
            Playlists = new HashSet<PlaylistData>();
        }

        [Key]
        public long TrackId { get; set; }
        public string Name { get; set; } = null!;
        public long? AlbumId { get; set; }
        public long MediaTypeId { get; set; }
        public long? GenreId { get; set; }
        public string? Composer { get; set; }
        public long Milliseconds { get; set; }
        public long? Bytes { get; set; }
        public byte[] UnitPrice { get; set; } = null!;

        public virtual AlbumData? Album { get; set; }
        public virtual GenreData? Genre { get; set; }
        public virtual MediaTypeData MediaType { get; set; } = null!;
        public virtual ICollection<InvoiceLineData> InvoiceLines { get; set; }

        public virtual ICollection<PlaylistData> Playlists { get; set; }
    }
}
