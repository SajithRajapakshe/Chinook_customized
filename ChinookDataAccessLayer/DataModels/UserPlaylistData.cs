namespace ChinookDataAccessLayer.Models;

public class UserPlaylistData
{
    public string UserId { get; set; }
    public long PlaylistId { get; set; }
    public ChinookUserData User { get; set; }
    public PlaylistData Playlist { get; set; }
}
