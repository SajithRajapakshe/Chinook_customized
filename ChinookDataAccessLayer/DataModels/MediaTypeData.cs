using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChinookDataAccessLayer.Models
{
    public partial class MediaTypeData
    {
        public MediaTypeData()
        {
            Tracks = new HashSet<TrackData>();
        }

        [Key]
        public long MediaTypeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<TrackData> Tracks { get; set; }
    }
}
