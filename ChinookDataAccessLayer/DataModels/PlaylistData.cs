using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChinookDataAccessLayer.Models
{
    public partial class PlaylistData
    {
        public PlaylistData()
        {
            Tracks = new HashSet<TrackData>();
        }

        [Key]
        public long PlaylistId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<TrackData> Tracks { get; set; }
        public virtual ICollection<UserPlaylistData> UserPlaylists { get; set; }

    }
}
