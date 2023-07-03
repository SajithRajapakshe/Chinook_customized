using ChinookDataAccessLayer.Models;

namespace Chinook.Models;

public class UserPlaylist
{
    public string UserId { get; set; }
    public long PlaylistId { get; set; }
    public ChinookUserData User { get; set; }
    public Playlist Playlist { get; set; }
}
